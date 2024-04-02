using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class CustomPolicy
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public short? Days { get; set; }
    
    [ForeignKey(nameof(Models.Employee))]
    public int? EmployeeId { get; set; }
    
    public Employee? Employee { get; set; }
}