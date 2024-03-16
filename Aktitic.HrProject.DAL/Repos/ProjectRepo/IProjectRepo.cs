using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IProjectRepo :IGenericRepo<Project>
{
    IQueryable<Project> GlobalSearch(string? searchKey);
}