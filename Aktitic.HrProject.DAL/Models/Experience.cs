namespace Aktitic.HrProject.DAL.Models;

public class Experience : BaseEntity
{
    public int Id { get; set; }
    public required string ExperienceLevel { get; set; }
    public required bool Status { get; set; }
}


