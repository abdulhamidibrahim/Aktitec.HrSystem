namespace Aktitic.HrProject.DAL.Models;

public class TicketFollowers
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}