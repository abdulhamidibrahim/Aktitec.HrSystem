using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IApplicationUserRepo :IGenericRepo<ApplicationUser>
{
    IQueryable<ApplicationUser> GlobalSearch(string? searchKey);

    Task<ApplicationUser> GetApplicationUserWithPermissionsAsync(int id);
    Task<IEnumerable<ApplicationUser>> GetAllWithPermissionsAsync();
    Task<IEnumerable<ApplicationUser>> GetAllWithEmployeesAsync();
}