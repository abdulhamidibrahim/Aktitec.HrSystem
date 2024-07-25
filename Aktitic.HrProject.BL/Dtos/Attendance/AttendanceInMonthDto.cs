using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class AttendanceInMonthDto
{
    public int Id { get; set; }
    
    public bool? Attended { get; set; }
    
    public DateOnly? Date { get; set; }
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
}