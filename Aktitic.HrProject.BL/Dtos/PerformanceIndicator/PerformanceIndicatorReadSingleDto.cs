using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class PerformanceIndicatorReadSingleDto
{
    public int Id { get; set; }
    public int? DesignationId { get; set; }
    public int? DepartmentId { get; set; }
    public EmployeeDto? AddedBy { get; set; }
    public DateTime? Date { get; set; }
    public string? CustomerExperience { get; set; }
    public string? Marketing { get; set; }
    public string? Management { get; set; }
    public string? Administration { get; set; }
    public string? PresentationSkill { get; set; }
    public string? QualityOfWork { get; set; }
    public string? Efficiency { get; set; }
    public string? Integrity { get; set; }
    public string? Professionalism { get; set; }
    public string? TeamWork { get; set; }
    public string? CriticalThinking { get; set; }
    public string? ConflictManagement { get; set; }
    public string? Attendance { get; set; }
    public string? MeetDeadline { get; set; }
    public bool? Status { get; set; }
}