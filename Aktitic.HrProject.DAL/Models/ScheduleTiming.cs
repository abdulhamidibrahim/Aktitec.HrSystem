namespace Aktitic.HrProject.DAL.Models;

public class ScheduleTiming : BaseEntity
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public DateTime? ScheduleDate1 { get; set; }
    public DateTime? ScheduleDate2 { get; set; }
    public DateTime? ScheduleDate3 { get; set; }
    public string? SelectTime1 { get; set; }
    public string? SelectTime2 { get; set; }
    public string? SelectTime3 { get; set; }
    
    public virtual Employee Employee { get; set; }
    public virtual Job Job { get; set; }
}


