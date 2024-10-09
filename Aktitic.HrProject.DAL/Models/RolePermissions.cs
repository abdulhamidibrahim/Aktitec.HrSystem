using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class RolePermissions : BaseEntity
{
    public int Id { get; set; }
    public bool Read { get; set; }
    public bool Edit { get; set; }
    public bool Add { get; set; }
    public bool Delete { get; set; }
    public bool Import { get; set; }
    public bool Export { get; set; }

    [ForeignKey(nameof(AppPages))]
    public string PageCode { get; set; }
    public AppPages AppPages { get; set; }
    
    [ForeignKey(nameof(CompanyRole))]
    public int CompanyRoleId { get; set; }
    public CompanyRole CompanyRole { get; set; }
}