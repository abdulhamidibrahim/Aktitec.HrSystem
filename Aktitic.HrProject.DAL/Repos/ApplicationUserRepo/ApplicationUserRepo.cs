using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class ApplicationUserRepo :GenericRepo<ApplicationUser>,IApplicationUserRepo
{
    private readonly HrSystemDbContext _context;

    public ApplicationUserRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<ApplicationUser> GlobalSearch(string? searchKey)
    {
        if (_context.ApplicationUsers != null)
        {
            var query = _context.ApplicationUsers
                .Include(a=>a.Employee)
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
                        x.Company.ToLower().Contains(searchKey) ||
                        x.Role!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.ApplicationUsers!.AsQueryable();
    }

    public async Task<ApplicationUser> GetApplicationUserWithPermissionsAsync(int id)
    {
        if (_context.ApplicationUsers != null)
            return await _context.ApplicationUsers!
                .Include(a => a.Permissions)
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
        return new ApplicationUser();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithPermissionsAsync()
    {
        if (_context.ApplicationUsers != null)
            return await _context.ApplicationUsers!
                .Include(a => a.Permissions)
                .AsQueryable()
                .ToListAsync();
        return await Task.FromResult<IEnumerable<ApplicationUser>>(new List<ApplicationUser>());
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllWithEmployeesAsync()
    {
        if (_context.ApplicationUsers != null)
            return await _context.ApplicationUsers!
                .Include(a => a.Employee)
                // .Include(a=>a.Permissions)
                .AsQueryable()
                .ToListAsync();
        return await Task.FromResult<IEnumerable<ApplicationUser>>(new List<ApplicationUser>());
    }
}
