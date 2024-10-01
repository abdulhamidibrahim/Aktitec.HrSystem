using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class PromotionReadDto
{
    public int Id { get; set; }
    public string? PromotionFrom { get; set; }
    public int? PromotionTo { get; set; }
    public DateOnly? Date { get; set; }
    public int? EmployeeId { get; set; }
}
