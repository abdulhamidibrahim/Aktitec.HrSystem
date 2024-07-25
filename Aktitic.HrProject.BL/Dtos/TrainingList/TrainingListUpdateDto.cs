using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class TrainingListUpdateDto
{
    public int? TrainingTypeId { get; set; }
    public int? TrainerId { get; set; }
    public int? EmployeeId { get; set; }
    public decimal? Cost { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
}
