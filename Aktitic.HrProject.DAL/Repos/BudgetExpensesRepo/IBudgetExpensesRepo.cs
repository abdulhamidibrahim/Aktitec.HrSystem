using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IBudgetExpensesRepo :IGenericRepo<BudgetExpenses>
{
    IQueryable<BudgetExpenses> GlobalSearch(string? searchKey);
    BudgetExpenses? GetWithCategory(int id);
    Task<List<BudgetExpenses>> GetAllWithCategoryAsync();
}