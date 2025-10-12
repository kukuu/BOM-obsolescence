using Microsoft.Extensions.Logging;
using BOM.Compliance.Application.Interfaces;
using BOM.Compliance.Application.Models.ExternalApis;
using System.Text.Json;

namespace BOM.Compliance.Application.Services;

public class EnhancedMLDataExtractionService : IMLDataExtractionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<EnhancedMLDataExtractionService> _logger;
    private readonly ISupabaseService _supabaseService;
    private readonly IVendorSearchService _vendorSearchService;

    public EnhancedMLDataExtractionService(
        IHttpClientFactory httpClientFactory,
        ILogger<EnhancedMLDataExtractionService> logger,
        ISupabaseService supabaseService,
        IVendorSearchService vendorSearchService)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _supabaseService = supabaseService;
        _vendorSearchService = vendorSearchService;
    }

    public async Task<ExtractionResult> ExtractComponentDataFromPdfAsync(Stream pdfStream, string filename)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("MLService");
            
            // Convert PDF to base64
            using var memoryStream = new MemoryStream();
            await pdfStream.CopyToAsync(memoryStream);
            var pdfBase64 = Convert.ToBase64String(memoryStream.ToArray());

            var request = new
            {
                pdf_data = pdfBase64,
                filename = filename,
                extract_metadata = true // NEW: Request enhanced metadata
            };

            var response = await client.PostAsJsonAsync("api/extract-component", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<EnhancedMLExtractionResponse>();
            
            return new ExtractionResult
            {
                Success = true,
                ReferenceId = result?.ReferenceId ?? string.Empty,
                ComponentName = result?.ComponentName ?? string.Empty,
                Specifications = result?.Specifications ?? new Dictionary<string, string>(),
                // NEW: Enhanced metadata
                ProductionYear = result?.ProductionYear ?? DateTime.UtcNow.Year,
                Manufacturer = result?.Manufacturer ?? string.Empty,
                PackageType = result?.PackageType ?? string.Empty,
                ManufacturerPartNumber = result?.ManufacturerPartNumber ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extracting data from PDF {Filename}", filename);
            return new ExtractionResult
            {
                Success = false,
                Errors = new List<string> { $"Extraction failed: {ex.Message}" }
            };
        }
    }

    public async Task<ComponentSubstitution> FindSubstituteComponentAsync(
        string referenceId, 
        ComponentRequirements requirements)
    {
        var substitution = new ComponentSubstitution 
        { 
            OriginalReferenceId = referenceId,
            Found = false 
        };

        try
        {
            // NEW: Algorithm Decision Tree for Substitution
            var searchResults = await ExecuteSubstitutionAlgorithm(referenceId, requirements);
            
            if (searchResults.Any())
            {
                var bestMatch = searchResults.OrderByDescending(x => x.ConfidenceScore).First();
                substitution = bestMatch;
                
                _logger.LogInformation(
                    "Found substitute for {ReferenceId}: {SubstituteId} with confidence {Confidence}",
                    referenceId, bestMatch.SubstituteReferenceId, bestMatch.ConfidenceScore);
            }
            else
            {
                _logger.LogWarning("No substitutes found for {ReferenceId}", referenceId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding substitute for {ReferenceId}", referenceId);
        }

        return substitution;
    }

    // NEW: Enhanced substitution algorithm with decision tree
    private async Task<List<ComponentSubstitution>> ExecuteSubstitutionAlgorithm(
        string referenceId, 
        ComponentRequirements requirements)
    {
        var results = new List<ComponentSubstitution>();
        
        // Step 1: Check internal database first
        var dbResults = await FindSubstitutesInDatabase(referenceId, requirements);
        results.AddRange(dbResults);

        // Step 2: If no good matches found, search vendors
        if (!results.Any(r => r.ConfidenceScore > 0.8))
        {
            var vendorResults = await SearchVendorCatalogs(referenceId, requirements);
            results.AddRange(vendorResults);
        }

        // Step 3: If still no matches, use LLM+RAG for intelligent search
        if (!results.Any(r => r.ConfidenceScore > 0.6))
        {
            var llmResults = await SearchWithLLMRAG(referenceId, requirements);
            results.AddRange(llmResults);
        }

        // Step 4: Final fallback - fuzzy matching with relaxed requirements
        if (!results.Any())
        {
            var fallbackResults = await FindFallbackAlternatives(referenceId, requirements);
            results.AddRange(fallbackResults);
        }

        return results;
    }

    private async Task<List<ComponentSubstitution>> FindSubstitutesInDatabase(
        string referenceId, ComponentRequirements requirements)
    {
        var substitutes = await _supabaseService.FindSimilarComponentsAsync(requirements);
        
        return substitutes.Select(s => new ComponentSubstitution
        {
            Found = true,
            OriginalReferenceId = referenceId,
            SubstituteReferenceId = s.ReferenceId,
            Manufacturer = s.Manufacturer,
            ProductionYear = s.ProductionYear,
            Compliance = s.Compliance,
            ConfidenceScore = CalculateDatabaseMatchScore(s, requirements),
            Source = "Internal Database",
            VendorName = "Internal",
            StockQuantity = s.StockQuantity,
            IsDirectReplacement = s.IsDirectReplacement
        }).ToList();
    }

    // NEW: Vendor search implementation
    private async Task<List<ComponentSubstitution>> SearchVendorCatalogs(
        string referenceId, ComponentRequirements requirements)
    {
        var vendorResults = new List<ComponentSubstitution>();
        
        try
        {
            // Search multiple vendor APIs
            var vendors = new[] { "DigiKey", "Mouser", "Arrow", "Newark" };
            
            foreach (var vendor in vendors)
            {
                var vendorComponents = await _vendorSearchService.SearchVendorAsync(
                    vendor, referenceId, requirements);
                
                vendorResults.AddRange(vendorComponents.Select(vc => new ComponentSubstitution
                {
                    Found = true,
                    OriginalReferenceId = referenceId,
                    SubstituteReferenceId = vc.ReferenceId,
                    Manufacturer = vc.Manufacturer,
                    ProductionYear = vc.ProductionYear,
                    Compliance = vc.Compliance,
                    ConfidenceScore = vc.ConfidenceScore,
                    Source = $"Vendor: {vendor}",
                    VendorName = vendor,
                    VendorPartNumber = vc.VendorPartNumber,
                    Price = vc.Price,
                    StockQuantity = vc.StockQuantity,
                    VendorUrl = vc.VendorUrl,
                    IsDirectReplacement = vc.IsDirectReplacement
                }));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching vendor catalogs for {ReferenceId}", referenceId);
        }

        return vendorResults;
    }

    // NEW: LLM + RAG pipeline for intelligent search
    private async Task<List<ComponentSubstitution>> SearchWithLLMRAG(
        string referenceId, ComponentRequirements requirements)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("LLMService");
            
            var request = new
            {
                original_reference_id = referenceId,
                requirements = requirements,
                search_strategy = "rag_with_vendor_lookup",
                include_compliance_data = true,
                include_pricing = true
            };

            var response = await client.PostAsJsonAsync("api/intelligent-substitute-search", request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LLMRAGSearchResponse>();
                
                return result?.Substitutes.Select(s => new ComponentSubstitution
                {
                    Found = true,
                    OriginalReferenceId = referenceId,
                    SubstituteReferenceId = s.ReferenceId,
                    Manufacturer = s.Manufacturer,
                    ProductionYear = s.ProductionYear,
                    Compliance = new ComplianceData
                    {
                        RoHSStatus = s.RoHSStatus,
                        REACHStatus = s.REACHStatus,
                        UL Certification = s.ULCertification,
                        CE marking = s.CEMarking
                    },
                    ConfidenceScore = s.ConfidenceScore,
                    Source = "LLM+RAG Pipeline",
                    VendorName = s.VendorName,
                    VendorPartNumber = s.VendorPartNumber,
                    Price = s.Price,
                    StockQuantity = s.StockQuantity,
                    VendorUrl = s.VendorUrl,
                    IsDirectReplacement = s.IsDirectReplacement,
                    Reasoning = s.Reasoning // Why this substitute was chosen
                }).ToList() ?? new List<ComponentSubstitution>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in LLM+RAG search for {ReferenceId}", referenceId);
        }

        return new List<ComponentSubstitution>();
    }

    private async Task<List<ComponentSubstitution>> FindFallbackAlternatives(
        string referenceId, ComponentRequirements requirements)
    {
        // Relax requirements and perform fuzzy matching
        var relaxedRequirements = new ComponentRequirements
        {
            Category = requirements.Category,
            RequiredSpecs = requirements.RequiredSpecs
                .Where(kvp => !kvp.Key.Contains("Tolerance")) // Relax tolerance
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            PackageType = requirements.PackageType
        };

        var results = await FindSubstitutesInDatabase(referenceId, relaxedRequirements);
        
        // Mark as fallback options
        foreach (var result in results)
        {
            result.Source = "Fallback Match";
            result.ConfidenceScore *= 0.8m; // Reduce confidence for fallbacks
        }

        return results;
    }

    private decimal CalculateDatabaseMatchScore(Material material, ComponentRequirements requirements)
    {
        decimal score = 0.7m; // Base score
        
        // Boost score for same manufacturer
        if (!string.IsNullOrEmpty(requirements.PreferredManufacturer) &&
            material.Manufacturer.Equals(requirements.PreferredManufacturer, StringComparison.OrdinalIgnoreCase))
        {
            score += 0.2m;
        }

        // Boost for recent production year
        if (material.ProductionYear >= DateTime.UtcNow.Year - 2)
        {
            score += 0.1m;
        }

        // Reduce score for obsolete components
        if (material.Status == MaterialStatus.Obsolete || 
            material.Status == MaterialStatus.EndOfLife)
        {
            score -= 0.3m;
        }

        return Math.Clamp(score, 0m, 1m);
    }
}