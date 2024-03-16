using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class EmployeeAttendanceDto
{
    public EmployeeDto? EmployeeDto { get; set; }
    public bool? Attended { get; set; }
    public DateOnly Date { get; set; }
}