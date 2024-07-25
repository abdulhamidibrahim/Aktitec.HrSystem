namespace Aktitic.HrProject.DAL.Models;

public class Event : BaseEntity
{
    public int Id { get; set; }    
    public string EventName { get; set; }
    public DateOnly StarDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string EventCategory { get; set; }
}
