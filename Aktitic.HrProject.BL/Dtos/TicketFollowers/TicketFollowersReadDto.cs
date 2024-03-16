using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class TicketFollowersReadDto
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public int? TicketId { get; set; }
    public TicketReadDto? Ticket { get; set; }
}
