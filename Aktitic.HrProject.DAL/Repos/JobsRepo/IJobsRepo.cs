using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IJobsRepo :IGenericRepo<Job>
{
    IQueryable<Job> GlobalSearch(string? searchKey);
    IEnumerable<Job> GetAllJobs();
}