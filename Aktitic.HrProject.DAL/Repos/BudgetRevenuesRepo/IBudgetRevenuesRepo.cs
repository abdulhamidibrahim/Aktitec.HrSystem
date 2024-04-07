using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IBudgetRevenuesRepo :IGenericRepo<BudgetRevenue>
{
    IQueryable<BudgetRevenue> GlobalSearch(string? searchKey);
    BudgetRevenue? GetWithCategory(int id);
    Task<List<BudgetRevenue>> GetAllWithCategoryAsync();
}