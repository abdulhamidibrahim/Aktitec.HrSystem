namespace Aktitic.HrProject.DAL.Models;

public class Termination : BaseEntity
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public string? Type { get; set; }
    public DateOnly? Date { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
}

