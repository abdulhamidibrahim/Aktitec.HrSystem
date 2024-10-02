using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Project : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    [ForeignKey(nameof(Client))]
    public int? ClientId { get; set; }
    public Client? Client { get; set; } = null!;
    public string Priority { get; set; }=string.Empty;
    
    
    // public int TeamId { get; set; }
    // public Team? Team { get; set; } = null!;
    
    public string? RateSelect { get; set; }
    public string? Rate { get; set; }
    public bool? Status { get; set; }
    public bool? Checked { get; set; }
    
    [ForeignKey(nameof(Employee))]
    public int? LeaderId { get; set; }

    public string? ProjectId { get; set; }
    public int[]? Team { get; set; }
    public Employee? Leader { get; set; }
    public List<Task> Tasks { get; set; } = new();
    // public List<Employee> Employees { get; set; } = new();
    public List<EmployeeProjects>? EmployeesProject { get; set; }
    public List<Department> Departments { get; set; } = new();
    public ICollection<Document> Files { get; set; }   = new List<Document>();
    
    [ForeignKey(nameof(TaskBoard))]
    public int? TaskBoardId { get; set; }
    public TaskBoard? TaskBoard { get; set; }

}