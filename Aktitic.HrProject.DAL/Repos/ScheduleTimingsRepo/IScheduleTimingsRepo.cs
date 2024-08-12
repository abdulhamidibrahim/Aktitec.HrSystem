using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IScheduleTimingsRepo :IGenericRepo<ScheduleTiming>
{
    IQueryable<ScheduleTiming> GlobalSearch(string? searchKey);
    
    Task<IEnumerable<ScheduleTiming>> GetAllScheduleTimings();
}