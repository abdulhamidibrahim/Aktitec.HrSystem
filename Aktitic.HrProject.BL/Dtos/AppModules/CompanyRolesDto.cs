using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class CompanyRolesDto
{
    public string Name { get; set; }
    public List<RolePermissionsDto> RolePermissions { get; set; }
}