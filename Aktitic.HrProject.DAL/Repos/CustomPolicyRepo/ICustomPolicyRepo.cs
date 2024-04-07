using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICustomPolicyRepo :IGenericRepo<CustomPolicy>
{
    // IQueryable<CustomPolicy> GlobalSearch(string? searchKey);
    
    List<CustomPolicy>? GetByType(string type);
    CustomPolicy? GetWithEmployee(int id);
    Task<List<CustomPolicy>>? GetAllWithEmployee();
    
}