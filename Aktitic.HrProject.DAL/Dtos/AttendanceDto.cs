using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Dtos;

public class AttendanceDto
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public DateTime? PunchIn { get; set; }

    public DateTime? PunchOut { get; set; }

    public string? Production { get; set; }

    public string? Break { get; set; }

    // public int? OvertimeId { get; set; }

    public int EmployeeId { get; set; }

    public  EmployeeDto Employee { get; set; }

    public  string? Overtime { get; set; }
    
}