using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPerformanceIndicatorRepo :IGenericRepo<PerformanceIndicator>
{
    IQueryable<PerformanceIndicator> GlobalSearch(string? searchKey);
    
    IQueryable<PerformanceIndicator> GetAllWithEmployees();
    IQueryable<PerformanceIndicator> GetWithEmployees(int id);
}