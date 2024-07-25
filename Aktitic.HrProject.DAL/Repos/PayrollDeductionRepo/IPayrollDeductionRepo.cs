using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPayrollDeductionRepo :IGenericRepo<PayrollDeduction>
{
    IQueryable<PayrollDeduction> GlobalSearch(string? searchKey);
    IQueryable<PayrollDeduction> GetWithEmployees(int id);
    Task<IQueryable<PayrollDeduction>> GetAllWithEmployees();
    
}