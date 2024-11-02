using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Company 
{
    public int Id { get; set; } //int
    public string CompanyName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Postal { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; } 

    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<ApplicationUser>? Users { get; set; }
    public ApplicationUser Manager { get; set; }
    
    public int ManagerId { get; set; }
    
    public ICollection<CompanyRole>? Roles { get; set; }
    public ICollection<CompanyModule>? CompanyModules { get; set; }
    
}