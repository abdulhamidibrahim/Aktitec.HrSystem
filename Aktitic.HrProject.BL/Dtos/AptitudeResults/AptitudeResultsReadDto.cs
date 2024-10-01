using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class AptitudeResultsReadDto
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public string? CategoryWiseMark { get; set; }
    public string? TotalMark { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public EmployeeDto Employee { get; set; }
    public JobsDto Job { get; set; }
}
