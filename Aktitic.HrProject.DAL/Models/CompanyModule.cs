using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class CompanyModule : BaseEntity
{
    public int Id { get; set; }
    public Company Company { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    
    public AppModule AppModule { get; set; }
    [ForeignKey(nameof(AppModule))]
    public int AppModuleId { get; set; }

    public ICollection<AppModule>? Modules { get; set; }
    
    public ICollection<RolePermissions> RolePermissions { get; set; }
}