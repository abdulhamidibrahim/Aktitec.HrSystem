namespace Aktitic.HrProject.BL.Dtos.EducationInformation;

public class FamilyInformationAddDto
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public string? Phone { get; set; }
    public DateOnly? DoB { get; set; }
}