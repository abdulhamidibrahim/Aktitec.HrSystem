using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class TimesheetReadDto
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public ProjectDto? ProjectDto { get; set; }
    public int? ProjectId { get; set; }

    public DateOnly? Deadline { get; set; }

    public short? AssignedHours { get; set; }

    public short? Hours { get; set; }

    public int? EmployeeId { get; set; }
    public EmployeeDto? EmployeeDto { get; set; }

    public string? Description { get; set; }

}
