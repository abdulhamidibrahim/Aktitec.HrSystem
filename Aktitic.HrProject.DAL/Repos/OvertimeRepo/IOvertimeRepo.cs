using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IOvertimeRepo :IGenericRepo<Overtime>
{
    IQueryable<Overtime> GlobalSearch(string? searchKey);
    // get overtimes with employee and approved by 
    Task<IEnumerable<Overtime>> GetOvertimesWithEmployeeAndApprovedBy();
    Task<Overtime> GetOvertimesWithEmployeeAndApprovedBy(int id);
    
}