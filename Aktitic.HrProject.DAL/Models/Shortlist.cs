namespace Aktitic.HrProject.DAL.Models;

public class Shortlist : BaseEntity
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public required string Status { get; set; }

    public Employee Employee { get; set; } 
    public Job Job { get; set; }    
}
