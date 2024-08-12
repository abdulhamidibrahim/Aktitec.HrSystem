using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class OfferApprovalDto
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required EmployeeDto Employee { get; set; }
    public required int JobId { get; set; }
    public required JobsDto Job { get; set; }
    public string? Pay { get; set; }
    public string? AnnualIp { get; set; }
    public string? LongTermIp { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}