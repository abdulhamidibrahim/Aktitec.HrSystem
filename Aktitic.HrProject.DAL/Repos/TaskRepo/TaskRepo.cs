using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskRepo :GenericRepo<Task>,ITaskRepo
{
    private readonly HrManagementDbContext _context;

    public TaskRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Task> GlobalSearch(string? searchKey)
    {
        if (_context.Tasks != null)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Title.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Priority!.ToLower().Contains(searchKey) ||
                        x.Date.ToString("dd/MM/yyyy").ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Tasks!.AsQueryable();
    }
}
