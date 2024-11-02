namespace Aktitic.HrProject.BL.Dtos.EducationInformation;

public class EducationInformationAddDto
{
    public int UserId { get; set; }
    public string? Institution { get; set; }
    public string? Subject { get; set; }
    public DateOnly? StartingDate { get; set; }
    public DateOnly? CompleteDate { get; set; }
    public string? Degree { get; set; }
    public string? Grade { get; set; }

}