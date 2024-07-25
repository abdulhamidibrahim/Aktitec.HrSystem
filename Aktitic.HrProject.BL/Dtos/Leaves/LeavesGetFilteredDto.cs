using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class LeavesGetFilteredDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }

    public bool? Approved { get; set; }

    public string? ApprovedBy { get; set; }
    public string? Status { get; set; }
    public EmployeeDto? Employee { get; set; }
    public int? ApprovedById { get; set; }
}
