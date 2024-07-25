namespace Aktitic.HrProject.DAL.Models;

public class Trainer : BaseEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string? Role { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool Status { get; set; }
       
}
