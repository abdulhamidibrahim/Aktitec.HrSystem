using System.Globalization;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ProjectRepo :GenericRepo<Project>,IProjectRepo
{
    private readonly HrSystemDbContext _context;

    public ProjectRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Project> GlobalSearch(string? searchKey)
    {
        if (_context.Projects != null)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
               if( DateTime.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate.Date == searchDate.Date ||
                            x.EndDate.Date == searchDate.Date);
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Priority.ToLower().Contains(searchKey) ||
                        x.RateSelect!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Projects!.AsQueryable();
    }

    public async Task<IQueryable<Project>> GetProjectWithEmployees(int id)
    {
        return await Task.FromResult(_context.Projects!
            .Where(x => x.Id == id)
            .Include(x => x.EmployeesProject)
            .ThenInclude(x => x.Employee));
    }

    public async Task<IQueryable<Project>> GetProjectWithEmployees()
    {
        return await Task.FromResult(_context.Projects!
            .Include(x => x.EmployeesProject)
            .ThenInclude(x => x.Employee));
    }
}
