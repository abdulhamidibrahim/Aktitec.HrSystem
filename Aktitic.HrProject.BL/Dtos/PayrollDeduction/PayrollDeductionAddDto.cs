using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;


namespace Aktitic.HrProject.BL;

public class PayrollDeductionAddDto
{
    public string? Name { get; set; }
    public bool UnitCalculation { get; set; }
    public string Assignee { get; set; }
    public int? EmployeeId { get; set; }
    public int UnitAmount { get; set; }
}
