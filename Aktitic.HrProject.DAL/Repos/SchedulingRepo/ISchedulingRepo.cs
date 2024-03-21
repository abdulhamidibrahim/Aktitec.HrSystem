using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ISchedulingRepo :IGenericRepo<Scheduling>
{
    // pagination(get 7 days start from today) - 
    List<Scheduling> GetSchedulingWithEmployees();
    
    IQueryable<Scheduling> GlobalSearch(string? searchKey);

}