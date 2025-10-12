namespace BOM.Compliance.Application.Interfaces;

public interface IVendorSearchService
{
    Task<List<VendorComponent>> SearchVendorAsync(string vendor, string referenceId, ComponentRequirements requirements);
    Task<List<VendorComponent>> SearchMultipleVendorsAsync(string referenceId, ComponentRequirements requirements);
}

public record VendorComponent
{
    public string ReferenceId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Manufacturer { get; init; } = string.Empty;
    public int ProductionYear { get; init; }
    public ComplianceData Compliance { get; init; } = new();
    public decimal ConfidenceScore { get; init; }
    public string VendorName { get; init; } = string.Empty;
    public string VendorPartNumber { get; init; } = string.Empty;
    public decimal? Price { get; init; }
    public int StockQuantity { get; init; }
    public string VendorUrl { get; init; } = string.Empty;
    public bool IsDirectReplacement { get; init; }
}