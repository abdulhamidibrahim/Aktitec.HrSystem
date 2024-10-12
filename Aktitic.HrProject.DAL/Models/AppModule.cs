namespace Aktitic.HrProject.DAL.Models;

public class AppModule
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ArabicName { get; set; }
    public ICollection<AppSubModule>? AppSubModules { get; set; }
    public ICollection<CompanyModule>? CompanyModules { get; set; }
    
}