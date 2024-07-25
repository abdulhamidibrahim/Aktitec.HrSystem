using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class GoalListUpdateDto
{
    public int? TypeId { get; set; }
    public string Subject { get; set; }
    public string TargetAchievement { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}
