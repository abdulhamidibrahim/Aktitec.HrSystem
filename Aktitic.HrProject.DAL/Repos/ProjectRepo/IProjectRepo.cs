using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IProjectRepo :IGenericRepo<Project>
{
    IQueryable<Project> GlobalSearch(string? searchKey);
    // project with employees 
    Task<IQueryable<Project>> GetProjectWithEmployees(int id);
    Task<IQueryable<Project>> GetProjectWithEmployees();
}