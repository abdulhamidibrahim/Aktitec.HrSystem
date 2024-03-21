using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class LeavesDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedById { get; set; }
    
    public string? Status { get; set; }

    public virtual EmployeeDto? ApprovedBy { get; set; }

    public virtual EmployeeDto? Employee { get; set; } 
}