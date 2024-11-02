namespace Aktitic.HrProject.BL.Dtos.EmergencyContact;

public class EmergencyContactReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? PrimaryName { get; set; }
    public string? PrimaryRelationship { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? PrimaryPhoneTwo { get; set; }
    public string? SecondaryName { get; set; }
    public string? SecondaryRelationship { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? SecondaryPhoneTwo { get; set; }
}