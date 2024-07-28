namespace Aktitic.HrProject.DAL.Models;

public class Event : BaseEntity
{
    public int Id { get; set; }    
    public string EventName { get; set; }
    public DateTime StarDate { get; set; }
    public DateTime EndDate { get; set; }
    public string EventCategory { get; set; }
}
