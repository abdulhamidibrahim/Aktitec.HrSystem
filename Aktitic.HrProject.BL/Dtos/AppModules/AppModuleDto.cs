using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class AppModuleDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string ArabicName { get; set; }

    public bool? Checked { get; set; } = false;
    public List<AppSubModuleDto>? SubModuleDto { get; set; }
}