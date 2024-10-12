namespace Aktitic.HrProject.DAL.Models;

public class AppSubModule
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ArabicName { get; set; }
    public int? AppModuleId { get; set; }
    public AppModule? AppModule { get; set; }
    public ICollection<AppPages>? AppPages { get; set; }
}