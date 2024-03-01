
namespace Aktitic.HrProject.DAL.Models;

public class Ticket
{
    
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Priority { get; set; }
    public string? Status { get; set; } 
    public string? Cc { get; set; }

    public int?  AssignedToEmployeeId { get; set; }
    public virtual Employee? AssignedTo { get; set; }

    public int? CreatedByEmployeeId { get; set; }
    public virtual Employee? CreatedBy { get; set; }
    
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    public ICollection<TicketFollowers> TicketFollowers { get; set; }=new List<TicketFollowers>();
    public ICollection<File> Files { get; set; }=new List<File>();
}