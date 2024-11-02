using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.DAL.Models;

public class Client : BaseEntity
    // : ApplicationUser
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; } = string.Empty;

    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";
    public string? ClientId { get; set; }

    public string? FileName { get; set; }=string.Empty;
    public byte[]? FileContent { get; set; }
    public string? FileExtension { get; set; }=string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public List<Ticket>? Tickets { get; set; }
    public bool? Status { get; set; }
    public List<Project>? Projects { get; set; }
    public string? UserName { get; set; }
    public string? PhotoUrl { get; set; }
    public ICollection<Permission>? Permissions { get; set; }
}