
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PromotionDto
{
    public int Id { get; set; }
    public string? PromotionFrom { get; set; }
    public DesignationDto? PromotionTo { get; set; }
    public DateOnly? Date { get; set; }
    public EmployeeDto? EmployeeId { get; set; }
}