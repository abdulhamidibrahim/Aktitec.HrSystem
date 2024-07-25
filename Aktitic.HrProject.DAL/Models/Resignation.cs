namespace Aktitic.HrProject.DAL.Models;

public class Resignation : BaseEntity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
    public DateOnly? ResignationDate { get; set; }
    public Employee? Employee { get; set; }
}
