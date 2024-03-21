using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

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
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Priority.ToLower().Contains(searchKey) ||
                        x.RateSelect!.ToLower().Contains(searchKey) ||
                        x.StartDate.ToString("dd/MM/yyyy").ToLower().Contains(searchKey) ||
                        x.EndDate.ToString("dd/MM/yyyy").ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Projects!.AsQueryable();
    }
}
