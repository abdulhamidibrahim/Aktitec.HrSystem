using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IApplicationUserRepo :IGenericRepo<ApplicationUser>
{
    IQueryable<ApplicationUser> GlobalSearch(string? searchKey);

    Task<ApplicationUser> GetApplicationUserWithPermissionsAsync(int id);
    Task<ApplicationUser?> GetUserAdmin(int? id);
    Task<IEnumerable<ApplicationUser>> GetAllWithPermissionsAsync();
    Task<IEnumerable<ApplicationUser>> GetAllWithEmployeesAsync();
    
    Task<List<int>> GetUserIdsByCompanyId(int companyId);
    bool IsAdmin(int adminId);
}