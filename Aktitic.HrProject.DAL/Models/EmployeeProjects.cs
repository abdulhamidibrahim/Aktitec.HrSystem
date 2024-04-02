using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

[Table("EmployeeProjects", Schema = "project")]
public class EmployeeProjects
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}