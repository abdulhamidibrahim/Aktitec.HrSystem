
namespace Aktitic.HrProject.DAL.Models;

public class Expenses
{
    public int? Id { get; set; }
    public string? ItemName { get; set; }
    public string? PurchaseFrom { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    
    public int? PurchasedById { get; set; }
    public Employee? PurchasedBy { get; set; }
    public float? Amount { get; set; }
    public string? PaidBy { get; set; }
    public string? Status { get; set; }
    public ICollection<File>? Attachments { get; set; }
}
