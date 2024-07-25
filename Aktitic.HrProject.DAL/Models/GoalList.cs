using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class GoalList : BaseEntity
{
    public int Id { get; set; }
    [ForeignKey("GoalType")]
    public int? TypeId { get; set; }
    public GoalType? GoalType { get; set; }
    public string Subject { get; set; }
    public string TargetAchievement { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}
