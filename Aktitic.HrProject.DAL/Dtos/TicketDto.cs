using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class TicketDto
{
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
    public  string? AssignedTo { get; set; }

    public int? CreatedByEmployeeId { get; set; }
    public  string? CreatedBy { get; set; }
    
    public int? ClientId { get; set; }
    public ClientDto? Client { get; set; }
}