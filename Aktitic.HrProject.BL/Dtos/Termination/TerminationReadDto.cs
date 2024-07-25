using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class TerminationReadDto
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public string? Type { get; set; }
    public DateOnly? Date { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
}
