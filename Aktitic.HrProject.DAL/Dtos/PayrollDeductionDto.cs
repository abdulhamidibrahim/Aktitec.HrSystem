
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PayrollDeductionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool UnitCalculation { get; set; }
    public string Assignee { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public int UnitAmount { get; set; }
}