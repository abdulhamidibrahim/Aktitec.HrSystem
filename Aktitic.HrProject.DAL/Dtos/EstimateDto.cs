using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Microsoft.VisualBasic.CompilerServices;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class EstimateDto
{
    // public int Id { get; set; }
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
    public IEnumerable<ItemDto>? Items { get; set; }
    public int? ClientId { get; set; }
    public ClientDto? Client { get; set; }
    public int? ProjectId { get; set; }
    public ProjectDto? Project { get; set; }
}