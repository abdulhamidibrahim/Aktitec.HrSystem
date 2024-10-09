using System;
using System.Linq;
using System.Threading.Tasks;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IApplicationUserRepo :IGenericRepo<ApplicationUser>
{
    IQueryable<ApplicationUser> GlobalSearch(string? searchKey);

    // Task<ApplicationUser> GetApplicationUserWithPermissionsAsync(int id);
    Task<ApplicationUser?> GetUserAdmin(int? id);
    // Task<IEnumerable<ApplicationUser>> GetAllWithPermissionsAsync();
    Task<IEnumerable<ApplicationUser>> GetAllWithEmployeesAsync(int companyId);
    
    Task<List<int>> GetUserIdsByCompanyId(int companyId);
    bool IsAdmin(int adminId);
    bool HasAccess( int companyId);
    Task AddConnection(string userId, string connectionId);
    Task RemoveConnection( string connectionId);
    public Task<int> GetUserIdByEmail(string email);
    Task<ApplicationUser?> GetUser(Func<ApplicationUser?, bool> predicate);
    Task<ApplicationUser?> GetUser(int id);
}