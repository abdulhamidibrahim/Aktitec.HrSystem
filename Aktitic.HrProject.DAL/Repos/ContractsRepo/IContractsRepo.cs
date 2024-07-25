using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IContractsRepo :IGenericRepo<Contract>
{
    IQueryable<Contract> GlobalSearch(string? searchKey);
    Task<IEnumerable<Contract>> GetAllWithEmployeeAndDepartment();
    
}