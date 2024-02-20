namespace Aktitic.HrProject.BL;

public class LeavesReadDto
{

    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedBy { get; set; }

}
