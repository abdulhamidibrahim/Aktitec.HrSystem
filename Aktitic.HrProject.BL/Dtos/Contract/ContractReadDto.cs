using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ContractReadDto
{
    public int Id { get; set; }
    
    public string ContractReference { get; set; }
    
    public int EmployeeId { get; set; }
    
    public string SalaryStructureType { get; set; }
    
    public DateOnly ContractStartDate { get; set; }
    
    public DateOnly ContractEndDate { get; set; }
    
    public int DepartmentId { get; set; }
    
    public string JobPosition { get; set; }
    
    public string ContractSchedule { get; set; }
    
    public string ContractType { get; set; }
    
    public decimal Wage { get; set; }
    
    public string Notes { get; set; }
    
    public string Status { get; set; }
    

}
