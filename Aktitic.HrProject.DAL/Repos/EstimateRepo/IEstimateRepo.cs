using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEstimateRepo :IGenericRepo<Estimate>
{
    IQueryable<Estimate> GlobalSearch(string? searchKey);
    Estimate GetEstimateWithItems(int id);
    Task<List<Estimate>> GetAllEstimateWithItems();
    
}