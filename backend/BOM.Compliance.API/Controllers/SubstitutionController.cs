using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BOM.Compliance.Application.Interfaces;

namespace BOM.Compliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubstitutionController : ControllerBase
{
    private readonly IMLDataExtractionService _mlService;
    private readonly ILogger<SubstitutionController> _logger;

    public SubstitutionController(IMLDataExtractionService mlService, ILogger<SubstitutionController> logger)
    {
        _mlService = mlService;
        _logger = logger;
    }

    [HttpPost("find-alternatives")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<ActionResult<SubstitutionResponse>> FindAlternatives(FindAlternativesRequest request)
    {
        try
        {
            var substitution = await _mlService.FindSubstituteComponentAsync(
                request.ReferenceId, 
                request.Requirements);

            var response = new SubstitutionResponse
            {
                OriginalReferenceId = request.ReferenceId,
                Substitutes = substitution.Found ? new List<ComponentSubstitution> { substitution } : new List<ComponentSubstitution>(),
                SearchTimestamp = DateTime.UtcNow,
                TotalAlternativesFound = substitution.Found ? 1 : 0
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding alternatives for {ReferenceId}", request.ReferenceId);
            return StatusCode(500, "An error occurred while searching for alternatives");
        }
    }

    [HttpPost("bulk-alternatives")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<ActionResult<BulkSubstitutionResponse>> FindBulkAlternatives(BulkAlternativesRequest request)
    {
        try
        {
            var results = new List<ComponentSubstitution>();
            
            foreach (var refId in request.ReferenceIds)
            {
                var substitution = await _mlService.FindSubstituteComponentAsync(refId, request.Requirements);
                if (substitution.Found)
                {
                    results.Add(substitution);
                }
            }

            var response = new BulkSubstitutionResponse
            {
                Results = results,
                TotalProcessed = request.ReferenceIds.Count,
                TotalFound = results.Count,
                SearchTimestamp = DateTime.UtcNow
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in bulk alternatives search");
            return StatusCode(500, "An error occurred during bulk search");
        }
    }
}

public record FindAlternativesRequest
{
    public string ReferenceId { get; init; } = string.Empty;
    public ComponentRequirements Requirements { get; init; } = new();
}

public record BulkAlternativesRequest
{
    public List<string> ReferenceIds { get; init; } = new();
    public ComponentRequirements Requirements { get; init; } = new();
}

public record SubstitutionResponse
{
    public string OriginalReferenceId { get; init; } = string.Empty;
    public List<ComponentSubstitution> Substitutes { get; init; } = new();
    public DateTime SearchTimestamp { get; init; }
    public int TotalAlternativesFound { get; init; }
}

public record BulkSubstitutionResponse
{
    public List<ComponentSubstitution> Results { get; init; } = new();
    public int TotalProcessed { get; init; }
    public int TotalFound { get; init; }
    public DateTime SearchTimestamp { get; init; }
}