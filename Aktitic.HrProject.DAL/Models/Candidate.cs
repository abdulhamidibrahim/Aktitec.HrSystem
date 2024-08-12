namespace Aktitic.HrProject.DAL.Models;

public class Candidate : BaseEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public required int EmployeeId { get; set; }
    public string? Phone { get; set; }
    
    public virtual Employee Employee { get; set; }    
}


