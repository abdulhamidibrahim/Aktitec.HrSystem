using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class ShiftDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public TimeOnly? MinStartTime { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? MaxStartTime { get; set; }

    public TimeOnly? MinEndTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? MaxEndTime { get; set; }

    public TimeOnly? BreakeTime { get; set; }

    public DateOnly? EndDate { get; set; }

    public short? RepeatEvery { get; set; }

    public bool? RecurringShift { get; set; }

    public bool? Indefinate { get; set; }

    public string? Tag { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual EmployeeDto? ApprovedByNavigation { get; set; }
}