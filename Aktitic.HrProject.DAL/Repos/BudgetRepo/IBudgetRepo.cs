using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IBudgetRepo :IGenericRepo<Budget>
{
    IQueryable<Budget> GlobalSearch(string? searchKey);
    Budget? GetWithDetails(int id);
    Task<List<Budget>> GetAllWithDetails();
}