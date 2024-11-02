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
    
    [ForeignKey(nameof(Models.Client))]
    public int? ClientId { get; set; }
    
    public Client? Client { get; set; }

    #region PersoInformation

    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PinCode { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Address { get; set; } 
    public bool? Gender { get; set; }
    public string? PassportNumber { get; set; } 
    public DateTime? PassportExpDate { get; set; }
    public string? Tel { get; set; } 
    public string? Nationality { get; set; } 
    public string? Religion { get; set; } 
    public string? MatritalStatus { get; set; } 
    public string? EmploymentSpouse { get; set; } 
    public int? ChildrenNumber { get; set; }
    
    [ForeignKey(nameof(ReportsTo))]
    public int? ReportsToId { get; set; }
    
    #endregion    
    

    public bool IsDeleted { get; set; }
    public string? ConnectionId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    

    #region NavigationProperties
    public ApplicationUser? ReportsTo { get; set; }
    public Company? ManagedCompany { get; set; }
    public Company? Company { get; set; }

    public ICollection<Asset>? Assets { get; set; }
    
    // public ICollection<Permission>? Permissions { get; set; }
    public ICollection<FileUsers>? FileUsers { get; set; }
    public ICollection<ChatGroupUser>? ChatGroupUsers { get; set; }
    public IEnumerable<Email>? SentEmails { get; set; }
    public IEnumerable<Email>? ReceivedEmails { get; set; }
    public IEnumerable<FamilyInformation>? FamilyInformations { get; set; }
    public IEnumerable<EmergencyContact>? EmergencyContacts { get; set; }
    public IEnumerable<EducationInformation>? EducationInformations { get; set; }

    #endregion
   
}