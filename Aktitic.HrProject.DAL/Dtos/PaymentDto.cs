using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Dtos;

public class PaymentDto
{
    public int? Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateOnly? PaidDate { get; set; }
    public float? PaidAmount { get; set; }
    public float? TotalAmount { get; set; }
    public string? BankName { get; set; }
    public string? PaymentType { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Iban { get; set; }
    public string? SwiftCode { get; set; }
    public string? Status { get; set; }
    public int? ClientId { get; set; }
    public string? Client { get; set; }
    
    public int? InvoiceId { get; set; }

}