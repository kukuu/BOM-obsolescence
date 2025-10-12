namespace BOM.Compliance.Application.Models.ExternalApis;

public class EnhancedMLExtractionResponse
{
    public bool Success { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    public string ComponentName { get; set; } = string.Empty;
    public Dictionary<string, string> Specifications { get; set; } = new();
    
    // NEW: Enhanced metadata fields
    public int ProductionYear { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public string ManufacturerPartNumber { get; set; } = string.Empty;
    public string PackageType { get; set; } = string.Empty;
    public string DatasheetVersion { get; set; } = string.Empty;
    public DateTime? DocumentDate { get; set; }
    public List<string> DetectedFeatures { get; set; } = new();
    public decimal ExtractionConfidence { get; set; }
}

public class LLMRAGSearchResponse
{
    public List<LLMSubstitute> Substitutes { get; set; } = new();
    public string SearchStrategy { get; set; } = string.Empty;
    public int TotalResults { get; set; }
}

public class LLMSubstitute
{
    public string ReferenceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int ProductionYear { get; set; }
    public decimal ConfidenceScore { get; set; }
    public string RoHSStatus { get; set; } = string.Empty;
    public string REACHStatus { get; set; } = string.Empty;
    public string ULCertification { get; set; } = string.Empty;
    public string CEMarking { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string VendorPartNumber { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int StockQuantity { get; set; }
    public string VendorUrl { get; set; } = string.Empty;
    public bool IsDirectReplacement { get; set; }
    public string Reasoning { get; set; } = string.Empty;
}