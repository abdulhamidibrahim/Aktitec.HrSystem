using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrProject.DAL.Models;

public class Invoice : BaseEntity
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public float? Tax { get; set; }
    public string? ClientAddress { get; set; }
    public string? BillingAddress { get; set; }
    public DateOnly? InvoiceDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public string? OtherInformation { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
    public string? InvoiceNumber { get; set; }
    public float? TotalAmount { get; set; }
    public float? Discount { get; set; }
    public float? GrandTotal { get; set; }
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    public IEnumerable<Item>? Items { get; set; }
}
