using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITerminationRepo :IGenericRepo<Termination>
{
    IQueryable<Termination> GlobalSearch(string? searchKey);
    
    IQueryable<Termination> GetAllWithEmployees();
    IQueryable<Termination> GetWithEmployees(int id);
}