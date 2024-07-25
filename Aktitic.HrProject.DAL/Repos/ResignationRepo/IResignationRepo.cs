using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IResignationRepo :IGenericRepo<Resignation>
{
    IQueryable<Resignation> GlobalSearch(string? searchKey);
    
    IQueryable<Resignation> GetAllWithEmployees();
    IQueryable<Resignation> GetWithEmployees(int id);
}