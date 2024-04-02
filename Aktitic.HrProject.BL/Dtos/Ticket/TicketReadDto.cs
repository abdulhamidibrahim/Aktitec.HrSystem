using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class TicketReadDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Priority { get; set; }
    public string? Status { get; set; } 
    public string? Cc { get; set; }
    public DateOnly? Date { get; set; }

    public string? TicketId { get; set; }
    public List<EmployeeDto>? Followers { get; set; }
    public DateOnly? LastReply { get; set; }
    public int?  AssignedToEmployeeId { get; set; }
    public EmployeeDto? AssignedTo { get; set; }
    public int? CreatedByEmployeeId { get; set; }
    public EmployeeDto? CreatedBy { get; set; }
    
    public int? ClientId { get; set; }
}
