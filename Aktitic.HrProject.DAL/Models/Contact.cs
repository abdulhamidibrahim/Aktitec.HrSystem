namespace Aktitic.HrProject.DAL.Models;

public class Contact : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public string Role { get; set; }
    public string Type { get; set; }
    public string? Image { get; set; }
    public bool Status { get; set; }
}