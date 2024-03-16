using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Project
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
    public decimal? Rate { get; set; }
    public bool? Status { get; set; }
    public bool? Checked { get; set; }
    
    public List<Task> Tasks { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public ICollection<File> Files { get; set; }   = new List<File>();
    
    

}