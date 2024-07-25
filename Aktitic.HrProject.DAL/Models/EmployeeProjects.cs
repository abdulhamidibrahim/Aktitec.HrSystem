using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

[Table("EmployeeProjects", Schema = "project")]
public class EmployeeProjects : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Employee))]
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}