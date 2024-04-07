using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class ProvidentFundsReadDto
{
    public int? Id { get; set; }
    public string? ProvidentType { get; set; }
    public double? EmployeeShareAmount { get; set; }
    public double? OrganizationShareAmount { get; set; }
    public double? EmployeeSharePercentage { get; set; }
    public double? OrganizationSharePercentage { get; set; }
    public string? Description { get; set; }
    public bool? Status { get; set; }
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
}
