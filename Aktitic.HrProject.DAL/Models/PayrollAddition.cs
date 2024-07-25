using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Models;

public class PayrollAddition : BaseEntity
{
        
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public bool? UnitCalculation { get; set; }
    public string? Assignee { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int? UnitAmount { get; set; }
}
