namespace Aktitic.HrProject.DAL.Models;

public class PerformanceAppraisal : BaseEntity
{

    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateOnly? Date { get; set; }
    public bool? Status { get; set; }

}