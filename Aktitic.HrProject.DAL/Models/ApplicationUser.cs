using System;
using System.ComponentModel.DataAnnotations.Schema;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Aktitic.HrProject.DAL.Models;

public class ApplicationUser : IdentityUser<int> ,ISoftDelete , IBaseEntity 
{
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }
    public CompanyRole Role { get; set; }
    public DateTime Date { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Password { get; set; }
    public Employee? Employee { get; set; }
    public int? EmployeeId { get; set; }
    public int? TenantId { get; set; }
    public bool IsManager { get; set; }
        
    public bool IsAdmin { get; set; }
    public bool HasAccess { get; set; } 
    public bool IsDeleted { get; set; }
    public string? ConnectionId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    

    #region NavigationProperties
    
    public Company? ManagedCompany { get; set; }
    public Company? Company { get; set; }

    public ICollection<Asset>? Assets { get; set; }
    
    // public ICollection<Permission>? Permissions { get; set; }
    public ICollection<FileUsers>? FileUsers { get; set; }
    public ICollection<ChatGroupUser>? ChatGroupUsers { get; set; }
    public IEnumerable<Email>? SentEmails { get; set; }
    public IEnumerable<Email>? ReceivedEmails { get; set; }
    public IEnumerable<FamilyInformation>? FamilyInformations { get; set; }

    #endregion
   
}