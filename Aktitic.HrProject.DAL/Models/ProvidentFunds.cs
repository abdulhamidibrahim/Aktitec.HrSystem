using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class ProvidentFunds : BaseEntity
{
    public int? Id { get; set; }
    public string? ProvidentType { get; set; }
    public double? EmployeeShareAmount { get; set; }
    public double? OrganizationShareAmount { get; set; }
    public double? EmployeeSharePercentage { get; set; }
    public double? OrganizationSharePercentage { get; set; }
    public string? Description { get; set; }
    public bool? Status { get; set; }
    
    [ForeignKey(nameof(Employee))]
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}

/*
 *  {
        "employeeId":1, //id
        "providentType": "Percentage of Basic Salary",
        "employeeShareAmount": "100",
        "organizationShareAmount": "200",
        "employeeSharePercentage": "3",
        "organizationSharePercentage": "5",
        "description": "test description up",
        "status": false,
    }
 */