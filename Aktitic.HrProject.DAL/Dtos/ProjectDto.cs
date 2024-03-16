using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Dtos;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public int? ClientId { get; set; }
    public ClientDto? Client { get; set; } = null!;
    public string Priority { get; set; }=string.Empty;
    
    
    // public int TeamId { get; set; }
    // public Team? Team { get; set; } = null!;
    
    public string? RateSelect { get; set; }
    public decimal? Rate { get; set; }
    public bool? Status { get; set; }
    public bool? Checked { get; set; }

}