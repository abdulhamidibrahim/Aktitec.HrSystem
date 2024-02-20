namespace Aktitic.HrProject.BL;

public class TimesheetUpdateDto
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public int? ProjectId { get; set; }

    public DateTime? Deadline { get; set; }

    public short? AssignedHours { get; set; }

    public short? Hours { get; set; }

    public int? EmployeeId { get; set; }

    public string? Description { get; set; }

}
