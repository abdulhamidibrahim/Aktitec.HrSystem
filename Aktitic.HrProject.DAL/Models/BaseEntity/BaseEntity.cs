using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class BaseEntity : ISoftDelete , IAuditable ,IMustHaveTenant
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public string? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    
    [ForeignKey(nameof(Tenant))]
    public int? TenantId { get; set; } 
    
    public virtual Company? Tenant { get; set; } 
}