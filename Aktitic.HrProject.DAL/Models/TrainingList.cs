namespace Aktitic.HrProject.DAL.Models;

public class TrainingList : BaseEntity
{
    public int Id { get; set; }
    public int? TrainingTypeId { get; set; }
    public TrainingType? TrainingType { get; set; }
    public int? TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public decimal? Cost { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}
