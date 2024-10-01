using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Microsoft.VisualBasic.CompilerServices;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class ExpensesDto
{
    public int? Id { get; set; }
    public string? ItemName { get; set; }
    public string? PurchaseFrom { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public string? PurchasedBy { get; set; }
    public float? Amount { get; set; }
    public string? PaidBy { get; set; }
    public string? Status { get; set; }
    public IEnumerable<FileDto>? Attachments { get; set; }
}