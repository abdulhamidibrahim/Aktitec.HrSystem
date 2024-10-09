using System.ComponentModel.DataAnnotations;

namespace Aktitic.HrProject.DAL.Models;

public class AppPages
{
    // public int Id { get; set; }
    public string Name { get; set; }
    [Key]
    public string Code { get; set; }
    public string ArabicName { get; set; }
    
    public int AppSubModuleId { get; set; }
    public AppSubModule? AppSubModule { get; set; }
}