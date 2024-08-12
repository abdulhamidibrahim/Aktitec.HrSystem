namespace Aktitic.HrProject.BL;

public class ExperienceDto
{
    public int Id { get; set; }
    public required string ExperienceLevel { get; set; }
    public required bool Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}