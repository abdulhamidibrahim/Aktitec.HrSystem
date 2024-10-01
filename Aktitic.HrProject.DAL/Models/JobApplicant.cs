namespace Aktitic.HrProject.DAL.Models;

public class JobApplicant : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required DateTime Date { get; set; }
    public string? Status { get; set; }
    public string? Resume { get; set; }

    public required int JobId { get; set; }
    public Job Job { get; set; }
    
}


