using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPayrollAdditionRepo :IGenericRepo<PayrollAddition>
{
    IQueryable<PayrollAddition> GlobalSearch(string? searchKey);
    IQueryable<PayrollAddition> GetWithEmployees(int id);
    Task<IQueryable<PayrollAddition>> GetAllWithEmployees();
    
}