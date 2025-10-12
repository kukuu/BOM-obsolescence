namespace BOM.Compliance.Domain.Entities;

public class Material
{
    public Guid Id { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public MaterialType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public MaterialStatus Status { get; set; }
    public ComplianceData Compliance { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    
    // NEW: Enhanced metadata fields
    public int ProductionYear { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public string ManufacturerPartNumber { get; set; } = string.Empty;
    public string PackageType { get; set; } = string.Empty;
    public DateTime? ObsoleteDate { get; set; }
    public DateTime? LastTimeBuyDate { get; set; }
    public string DatasheetUrl { get; set; } = string.Empty;
    public List<AlternativeComponent> Alternatives { get; set; } = [];
    
    // Navigation properties
    public List<MaterialSpecification> Specifications { get; set; } = [];
    public List<LifecycleEvent> LifecycleEvents { get; set; } = [];
}

// NEW: Alternative component entity
public class AlternativeComponent
{
    public Guid Id { get; set; }
    public Guid OriginalMaterialId { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int ProductionYear { get; set; }
    public decimal ConfidenceScore { get; set; }
    public string Source { get; set; } = string.Empty; // "Vendor", "LLM", "Database"
    public DateTime FoundDate { get; set; }
    public ComplianceData Compliance { get; set; } = new();
    public bool IsRecommended { get; set; }
    
    // Vendor information
    public string VendorName { get; set; } = string.Empty;
    public string VendorPartNumber { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int StockQuantity { get; set; }
    public string VendorUrl { get; set; } = string.Empty;
}