
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class RevisorDto
{
    public EmployeeDto Employee { get; set; }
    public int EmployeeId { get; set; }
    public string? Notes { get; set; }
    public bool IsReviewed { get; set; }
    public DateTime? RevisionDate { get; set; }
}