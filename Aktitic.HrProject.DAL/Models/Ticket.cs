
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Ticket : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Priority { get; set; }
    public string? Status { get; set; } 
    public string? Cc { get; set; }
    public DateOnly? Date { get; set; }
    public string? TicketId { get; set; }
    public int[]? Followers { get; set; }
    public DateOnly? LastReply { get; set; }
    public int?  AssignedToEmployeeId { get; set; }
    public virtual Employee? AssignedTo { get; set; }

    public int? CreatedByEmployeeId { get; set; }
    public virtual Employee? CreatedBy { get; set; }
    
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    public ICollection<TicketFollowers> TicketFollowers { get; set; }=new List<TicketFollowers>();
    public ICollection<Document> Files { get; set; }=new List<Document>();
}