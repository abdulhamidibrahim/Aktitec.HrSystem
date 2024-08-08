namespace Aktitic.HrProject.DAL.Models;

public class Asset : BaseEntity
{
    public int Id { get; set; }
    public required string AssetName { get; set; }
    public required string AssetId { get; set; }
    public DateTime PurchaseFrom { get; set; }
    public DateTime PurchaseTo { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public string? Supplier { get; set; }
    public string? Condition { get; set; }
    public string? Value { get; set; }
    public string? Description { get; set; }
    public decimal? Warranty { get; set; }
    public required int UserId { get; set; }
    public required string Status { get; set; }
    
    public ApplicationUser User { get; set; }
}

