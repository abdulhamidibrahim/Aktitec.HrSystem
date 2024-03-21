namespace Aktitic.HrProject.BL;

public class LeavesUpdateDto
{

    public int EmployeeId { get; set; }

    public string? Type { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Reason { get; set; }

    public short? Days { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedBy { get; set; }

    public string? Status { get; set; }
}
