namespace Aktitic.HrProject.DAL.Models;

public class FamilyInformation : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Relationship { get; set; }
    public string Phone { get; set; }
    public string DoB { get; set; }
    public ApplicationUser User { get; set; }
}

