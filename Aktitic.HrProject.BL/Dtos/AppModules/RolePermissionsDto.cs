namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class RolePermissionsDto
{
    // public int CompanyId { get; set; }
    // public int ModuleId { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanRead { get; set; }
    public bool CanExport { get; set; }
    public bool CanImport { get; set; }
    
    public required string AppPageCode { get; set; }
}