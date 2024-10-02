using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;


namespace Aktitic.HrProject.BL;

public class SalaryAddDto
{
   
    public int? EmployeeId { get; set; }
    public double? NetSalary { get; set; }
    public DateOnly? Date { get; set; }
    public double? BasicEarnings { get; set; }
    public double? Tds { get; set; }
    public double? Da { get; set; }
    public double? Esi { get; set; }
    public double? Hra { get; set; }
    public double? Pf { get; set; }
    public double? Conveyance { get; set; }
    public double? Leave { get; set; }
    public double? Allowance { get; set; }
    public double? ProfTax { get; set; }
    public double? MedicalAllowance { get; set; }
    public double? LabourWelfare { get; set; }
    public double? Fund { get; set; }
    public string? Others1 { get; set; }
    public string? Others2 { get; set; }
    public string? PayslipId { get; set; }
}
