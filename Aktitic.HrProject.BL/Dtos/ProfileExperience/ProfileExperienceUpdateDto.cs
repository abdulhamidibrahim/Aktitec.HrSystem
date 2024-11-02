namespace Aktitic.HrProject.BL.Dtos.ProfileExperience;

public class ProfileExperienceUpdateDto
{
    public int UserId { get; set; }
    public string? CompanyName { get; set; }
    public string? JopPosition { get; set; }
    public string? Location { get; set; }
    public DateOnly? PeriodFrom { get; set; }
    public DateOnly? PeriodTo { get; set; }
}