namespace Aktitic.HrProject.BL;

public class OvertimeReadDto
{
    public int Id { get; set; }
    public DateOnly? OtDate { get; set; }

    public byte? OtHours { get; set; }

    public string? OtType { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public int? EmployeeId { get; set; }
}
