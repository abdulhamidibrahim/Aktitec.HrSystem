namespace Aktitic.HrProject.DAL.Models;

public class Job : BaseEntity
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public required int DepartmentId { get; set; }
    public string? JobLocation { get; set; }
    public string? NoOfVacancies { get; set; }
    public int Age { get; set; }
    public string? Experience { get; set; }
    public decimal SalaryFrom { get; set; }
    public decimal SalaryTo { get; set; }
    public required string JobType { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public Department Department { get; set; }
    public ICollection<Shortlist>? Shortlists { get; set; }
    public ICollection<OfferApproval>? OfferApprovals { get; set; }
    public ICollection<ScheduleTiming>? ScheduleTimings { get; set; }
    public ICollection<AptitudeResult>? AptitudeResults { get; set; }
    public ICollection<JobApplicant>? JobApplicants { get; set; }
}


