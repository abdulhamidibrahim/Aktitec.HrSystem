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

    public int?  AssignedToEmployeeId { get; set; }
    public virtual EmployeeDto? AssignedTo { get; set; }

    public int? CreatedByEmployeeId { get; set; }
    public virtual EmployeeDto? CreatedBy { get; set; }
    
    public int? ClientId { get; set; }
    public ClientDto? Client { get; set; }
}