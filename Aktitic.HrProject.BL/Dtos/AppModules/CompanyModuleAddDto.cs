namespace Aktitic.HrProject.BL.Dtos.AppModules;

public abstract record CompanyModuleAddDto
{
    public int ComapnyId { get; set; }
    // public int ModuleId { get; set; }
    public List<AppModuleDto> Modules { get; set; }
}