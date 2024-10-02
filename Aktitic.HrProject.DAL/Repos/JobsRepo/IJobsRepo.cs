using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IJobsRepo :IGenericRepo<Job>
{
    IQueryable<Job> GlobalSearch(string? searchKey);
    Task<IEnumerable<Job>> GetAllJobs();
    Task<IEnumerable<Job>> GetByCategory(string? category);
    
    Task<object> GetTotalCount();
}