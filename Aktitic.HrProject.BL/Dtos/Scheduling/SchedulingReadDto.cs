using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class SchedulingReadDto
{
    public int Id { get; set; }
    public int? DepartmentId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? Date { get; set; }

    public int? ShiftId { get; set; }

    public TimeOnly? MinStartTime { get; set; }

    public TimeOnly? MaxStartTime { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? MinEndTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? MaxEndTime { get; set; }

    public TimeOnly? BreakTime { get; set; }

    public short? RepeatEvery { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public EmployeeDto? Employee { get; set; }

}
