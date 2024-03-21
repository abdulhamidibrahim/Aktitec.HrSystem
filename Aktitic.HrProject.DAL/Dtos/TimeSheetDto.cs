using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class TimeSheetDto
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public int? ProjectId { get; set; }

    public DateOnly? Deadline { get; set; }

    public short? AssignedHours { get; set; }

    public short? Hours { get; set; }

    public int? EmployeeId { get; set; }

    public string? Description { get; set; }

    public virtual EmployeeDto IdNavigation { get; set; } = null!;
    public virtual ProjectDto ProjectDto { get; set; } = null!;
}
    
