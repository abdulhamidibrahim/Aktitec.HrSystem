
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class TerminationDto
{
    public int Id { get; set; }
    public EmployeeDto? EmployeeId { get; set; }
    public string? Type { get; set; }
    public DateOnly? Date { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
}