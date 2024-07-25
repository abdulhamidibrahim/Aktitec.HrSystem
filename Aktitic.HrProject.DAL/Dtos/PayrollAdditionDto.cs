
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PayrollAdditionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
    public bool? UnitCalculation { get; set; }
    public string? Assignee { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public int? UnitAmount { get; set; }
}