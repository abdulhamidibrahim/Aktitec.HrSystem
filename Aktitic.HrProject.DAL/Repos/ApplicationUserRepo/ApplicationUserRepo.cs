using System;
using System.Linq;
using System.Threading.Tasks;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class ApplicationUserRepo(HrSystemDbContext context)
    : GenericRepo<ApplicationUser>(context), IApplicationUserRepo
{
    private readonly HrSystemDbContext _context = context;

    public IQueryable<ApplicationUser> GlobalSearch(string? searchKey)
    {
        if (_context.ApplicationUsers != null)
        {
            var query = _context.ApplicationUsers
                .Include(a=>a.Employee)
                .Include(a=>a.Company)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.FirstName!.ToLower().Contains(searchKey) ||
                        x.LastName!.ToLower().Contains(searchKey) ||
                        x.UserName!.ToLower().Contains(searchKey) ||
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.PhoneNumber!.ToLower().Contains(searchKey) ||
                        x.Company.CompanyName.Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.ApplicationUsers!.AsQueryable();
    }

    // public async Task<ApplicationUser> GetApplicationUserWithPermissionsAsync(int id)
    // {
        // if (_context.ApplicationUsers != null)
            // return await _context.ApplicationUsers!
                // .Include(a => a.Permissions)
                // .AsQueryable()
                // .FirstOrDefaultAsync(x => x.Id == id);
        // return new ApplicationUser();
    // }

    public async Task<ApplicationUser?> GetUserAdmin(int? companyId)
    {
        if (_context.ApplicationUsers != null)
        {
            return await _context.ApplicationUsers
                .Where(x => x.TenantId == companyId && x.IsManager)
                .FirstOrDefaultAsync();
        }
        return (new ApplicationUser());
    }

    // public async Task<IEnumerable<ApplicationUser>> GetAllWithPermissionsAsync()
    // {
        // if (_context.ApplicationUsers != null)
            // return await _context.ApplicationUsers!
                // .Include(a => a.Permissions)
                // .AsQueryable()
                // .ToListAsync();
        // return await Task.FromResult<IEnumerable<ApplicationUser>>(new List<ApplicationUser>());
    // }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithEmployeesAsync(int companyId)
    {
        if (_context.ApplicationUsers != null)
            return await _context.ApplicationUsers!
                .Where(x=>x.TenantId == companyId && companyId != 0)
                .Include(a => a.Employee)
                // .Include(a=>a.Permissions)
                .AsQueryable()
                .ToListAsync();
        return await Task.FromResult<IEnumerable<ApplicationUser>>(new List<ApplicationUser>());
    }

    public async Task<List<int>> GetUserIdsByCompanyId(int companyId)
    {
        if (_context.ApplicationUsers != null)
            return await _context.ApplicationUsers!
                .Where(x => x.TenantId == companyId)
                .Select(x => x.Id)
                .ToListAsync();
        return (new List<int>());
    }

    public bool IsAdmin(int adminId)
    {
        
        if (_context.ApplicationUsers != null)
            return _context.ApplicationUsers.Any(x => x.Id == adminId && x.IsManager ==true);
        return false;
    }

    public bool HasAccess(int companyId)
    {
        if (_context.ApplicationUsers != null)
            return _context.ApplicationUsers.Include(x=>x.Company).ThenInclude(x=>x).Any(x => x.TenantId == companyId);
        return false;
    }

    public Task AddConnection(string userId, string connectionId)
    {
        var user = _context.ApplicationUsers.Where(u=>u.Id == int.Parse(userId));
        if (user != null)
        {
            user.First().ConnectionId = connectionId;
            return _context.SaveChangesAsync();
        }
        return Task.CompletedTask;
    }

    public Task RemoveConnection(string connectionId)
    {
        var user = _context.ApplicationUsers.Where(u => u.ConnectionId == connectionId);
        if (user != null)
        {
            user.First().ConnectionId = null;
            return _context.SaveChangesAsync();
        }
        return Task.CompletedTask;
    }

    public async Task<ApplicationUser?> GetUser(Func<ApplicationUser?, bool> predicate)
    {
        if (_context.ApplicationUsers != null)
            return  (_context.ApplicationUsers.FirstOrDefault(predicate));
        return  (new ApplicationUser());
    }

    public Task<ApplicationUser?> GetUser(int id)
    {
        if (_context.ApplicationUsers != null)
            return _context.ApplicationUsers!
                .Include(a => a.Employee)
                .Include(a=>a.Role)
                .ThenInclude(a=>a.RolePermissions)
                .Include(a => a.Company)
                .FirstOrDefaultAsync(x => x.Id == id);
        return Task.FromResult<ApplicationUser?>(null);
    }

    public Task<int> GetUserIdByEmail(string email)
    {
        return _context.Users!
            .AsSplitQuery()
            .Where(x => x.NormalizedEmail == email.ToUpper())
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

}
