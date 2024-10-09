using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class CompanyRole : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    
    public Company Company { get; set; }
    public ICollection<RolePermissions>? RolePermissions { get; set; }
    public ApplicationUser? User { get; set; }
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
}