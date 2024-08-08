namespace Aktitic.HrProject.DAL.Models;

public sealed class OfferApproval : BaseEntity
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public string? Pay { get; set; }
    public string? AnnualIp { get; set; }
    public string? Status { get; set; }
    
    public Employee Employee { get; set; }
    public Job Job { get; set; }
}


