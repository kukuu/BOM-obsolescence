public record ExtractionResult
{
    public bool Success { get; init; }
    public string ReferenceId { get; init; } = string.Empty;
    public string ComponentName { get; init; } = string.Empty;
    public Dictionary<string, string> Specifications { get; init; } = new();
    public List<string> Errors { get; init; } = new();
    
    // NEW: Enhanced metadata
    public int ProductionYear { get; init; }
    public string Manufacturer { get; init; } = string.Empty;
    public string ManufacturerPartNumber { get; init; } = string.Empty;
    public string PackageType { get; init; } = string.Empty;
    public decimal ExtractionConfidence { get; init; }
}

public record ComponentSubstitution
{
    public bool Found { get; init; }
    public string OriginalReferenceId { get; init; } = string.Empty;
    public string SubstituteReferenceId { get; init; } = string.Empty;
    public string Manufacturer { get; init; } = string.Empty;
    public int ProductionYear { get; init; }
    public ComplianceData Compliance { get; init; } = new();
    public decimal ConfidenceScore { get; init; }
    public string Source { get; init; } = string.Empty;
    
    // NEW: Vendor information
    public string VendorName { get; init; } = string.Empty;
    public string VendorPartNumber { get; init; } = string.Empty;
    public decimal? Price { get; init; }
    public int StockQuantity { get; init; }
    public string VendorUrl { get; init; } = string.Empty;
    public bool IsDirectReplacement { get; init; }
    public string? Reasoning { get; init; } // Why this substitute was chosen
}