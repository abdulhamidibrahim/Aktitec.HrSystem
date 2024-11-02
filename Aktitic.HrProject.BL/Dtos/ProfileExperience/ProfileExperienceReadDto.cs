namespace Aktitic.HrProject.BL.Dtos.ProfileExperience;

public class ProfileExperienceReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CompanyName { get; set; }
    public string? JobPosition { get; set; }
    public string? Location { get; set; }
    public DateOnly? PeriodFrom { get; set; }
    public DateOnly? PeriodTo { get; set; }
}