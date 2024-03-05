using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Client 
    // : ApplicationUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string? FileName { get; set; }=string.Empty;
    public byte[]? FileContent { get; set; }
    public string? FileExtension { get; set; }=string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;

    public List<Ticket>? Tickets { get; set; }
    public Project? Project { get; set; }
}