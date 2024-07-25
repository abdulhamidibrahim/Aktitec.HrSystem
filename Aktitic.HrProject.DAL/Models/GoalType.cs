namespace Aktitic.HrProject.DAL.Models;

public class GoalType : BaseEntity
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}
