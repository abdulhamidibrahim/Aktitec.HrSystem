using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ProvidentFundsAddDto
{
    public string? ProvidentType { get; set; }
    public double? EmployeeShareAmount { get; set; }
    public double? OrganizationShareAmount { get; set; }
    public double? EmployeeSharePercentage { get; set; }
    public double? OrganizationSharePercentage { get; set; }
    public string? Description { get; set; }
    public bool? Status { get; set; }
    public int? EmployeeId { get; set; }
   
}
