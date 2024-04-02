using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IExpensesRepo :IGenericRepo<Expenses>
{
    IQueryable<Expenses> GlobalSearch(string? searchKey);
    Expenses GetEstimateWithEmployee(int id);
    Task<List<Expenses>> GetAllEstimateWithEmployees();
    
}