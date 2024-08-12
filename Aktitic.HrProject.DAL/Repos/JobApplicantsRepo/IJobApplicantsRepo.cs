using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IJobApplicantsRepo :IGenericRepo<JobApplicant>
{
    IQueryable<JobApplicant> GlobalSearch(string? searchKey);
}