using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class OvertimeDto
{
    public int Id { get; set; }

    public DateOnly? OtDate { get; set; }

    public byte? OtHours { get; set; }

    public string? OtType { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? ApprovedBy { get; set; }

    public string? Employee { get; set; }

}