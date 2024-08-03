using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

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
    bool HasAccess( int companyId);
    Task AddConnection(string userId, string connectionId);
    Task RemoveConnection( string connectionId);
    Task<ApplicationUser?> GetUser(Func<ApplicationUser?, bool> predicate);
}