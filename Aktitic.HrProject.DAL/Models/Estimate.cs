using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Estimate : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Email { get; set; }
    public string? ClientAddress { get; set; }
    public string? BillingAddress { get; set; }
    public DateOnly? EstimateDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public string? OtherInformation { get; set; }
    public string? Status { get; set; }
    public string? EstimateNumber { get; set; }
    public float? TotalAmount { get; set; }
    public float? Discount { get; set; }
    public float? Tax { get; set; }
    public float? GrandTotal { get; set; }
    public IEnumerable<Item>? Items { get; set; }
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}