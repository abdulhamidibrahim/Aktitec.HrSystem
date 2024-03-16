using Aktitic.HrProject.BL.Dtos.Employee;

namespace Aktitic.HrProject.BL;

public class TicketFollowersAddDto
{
    
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
   
    public int? TicketId { get; set; }
    
}
