
namespace Aktitic.HrProject.DAL.Dtos;

public class GoalListDto
{
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string? GoalType { get; set; }
        public string Subject { get; set; }
        public string TargetAchievement { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
}  
