using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class EmployeeAttendanceDto
{
    public EmployeeDto? EmployeeDto { get; set; }
    public IEnumerable<AttendanceDto> AttendanceDto { get; set; }
    public bool? Attended { get; set; }
    public DateOnly Date { get; set; }
}