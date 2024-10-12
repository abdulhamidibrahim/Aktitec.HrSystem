namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class AppSubModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ArabicName { get; set; }

    public bool? Checked { get; set; } = false;
    public List<AppPageDto>? PageDto { get; set; }
}