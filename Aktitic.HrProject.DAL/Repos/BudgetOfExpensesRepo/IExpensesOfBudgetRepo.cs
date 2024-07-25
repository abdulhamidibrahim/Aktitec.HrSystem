using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IExpensesOfBudgetRepo :IGenericRepo<ExpensesOfBudget>
{
    IQueryable<Estimate> GlobalSearch(string? searchKey);
    Estimate GetEstimateWithItems(int id);
    Task<List<Estimate>> GetAllEstimateWithItems();
    
}