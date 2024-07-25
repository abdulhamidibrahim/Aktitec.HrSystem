using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Dtos;

public class TrainingListDto
{
    public int Id { get; set; }
    public int? TrainingTypeId { get; set; }
    public string? TrainingType { get; set; }
    public int? TrainerId { get; set; }
    public string? Trainer { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public decimal? Cost { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}  
