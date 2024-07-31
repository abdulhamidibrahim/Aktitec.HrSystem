using System.ComponentModel.DataAnnotations.Schema;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Aktitic.HrProject.DAL.Models;

public class ApplicationUser : IdentityUser<int> ,ISoftDelete , IAuditable
{
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    // public string Company { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Password { get; set; }
    
    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<FileUsers>? FileUsers { get; set; }
    public ICollection<ChatGroupUser>? ChatGroupUsers { get; set; }
    
    public Employee? Employee { get; set; }
    public int? EmployeeId { get; set; }

    public Company? Company { get; set; }
    
    [ForeignKey(nameof(Company))]
    public int? CompanyId { get; set; }
    
    // public Client? Client { get; set; }
    // public int? ClientId { get; set; }
    public bool IsManager { get; set; }
        
    public bool IsAdmin { get; set; }
    public bool HasAccess { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}