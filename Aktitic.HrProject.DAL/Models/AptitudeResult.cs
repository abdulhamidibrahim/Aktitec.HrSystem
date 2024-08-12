namespace Aktitic.HrProject.DAL.Models;

public class AptitudeResult : BaseEntity
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public string? CategoryWiseMark { get; set; }
    public string? TotalMark { get; set; }
    public string? Status { get; set; }
    
    public virtual Employee Employee { get; set; }
    public virtual Job Job { get; set; }
}


