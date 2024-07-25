using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPerformanceAppraisalRepo :IGenericRepo<PerformanceAppraisal>
{
    IQueryable<PerformanceAppraisal> GlobalSearch(string? searchKey);
    
    IQueryable<PerformanceAppraisal> GetAllWithEmployees();
    IQueryable<PerformanceAppraisal> GetWithEmployees(int id);
}