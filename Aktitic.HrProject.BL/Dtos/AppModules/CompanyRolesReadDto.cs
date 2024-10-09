namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class CompanyRolesReadDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CompanyId { get; set; }
    public List<RolePermissionsDto>? RolePermissions { get; set; }
}