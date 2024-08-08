namespace Aktitic.HrProject.BL;

public class AssetsDto
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
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}