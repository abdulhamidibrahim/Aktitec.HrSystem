using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class ProjectReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public int? ClientId { get; set; }
    public string Priority { get; set; }=string.Empty;
    public EmployeeDto? TeamLeader { get; set; }
    public int? LeaderId { get; set; }
    // public int TeamId { get; set; }
    // public Team? Team { get; set; } = null!;
    public List<EmployeeDto>? Team { get; set; }
    public string? ProjectId { get; set; }
    public string? RateSelect { get; set; }
    public string? Rate { get; set; }
    public bool? Status { get; set; }
    // public bool? Checked { get; set; }

}
