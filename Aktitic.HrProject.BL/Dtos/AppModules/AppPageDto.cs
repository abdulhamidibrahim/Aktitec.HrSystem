namespace Aktitic.HrProject.BL.Dtos.AppModules;

public class AppPageDto
{
    public string Name { get; set; }
    public string ArabicName { get; set; }
    public string Code { get; set; }

    public bool  Read { get; set; }
    public bool  Add { get; set; }
    public bool  Update { get; set; }
    public bool  Delete { get; set; }
    public bool  Export { get; set; }
    public bool  Import { get; set; }
    public bool?  Checked { get; set; } = false;
    
    // public List<RolePermissionsDto>? Roles { get; set; }
}