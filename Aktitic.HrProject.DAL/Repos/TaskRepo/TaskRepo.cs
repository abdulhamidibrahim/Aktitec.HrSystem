using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskRepo :GenericRepo<Task>,ITaskRepo
{
    private readonly HrSystemDbContext _context;

    public TaskRepo(HrSystemDbContext context) : base(context)
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
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date.Date == searchDate.Date);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Text.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Priority!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Tasks!.AsQueryable();
    }

    public IEnumerable<Task>? GetTaskByProjectId(int projectId)
    {
        return _context.Tasks?.Where(x => x.ProjectId == projectId).ToList();
    }

    public IEnumerable<Task>? GetTaskByCompleted(bool completed)
    {
        return _context.Tasks?.Where(x => x.Completed == completed).ToList();
    }

    public IEnumerable<Task>? GetAllTasksWithEmployeeAndProject()
    {
        return _context.Tasks?
            .Include(x=>x.AssignEmployee)
            .Include(x=>x.Project)
            .ToList();
    }

    public Task? GetTaskWithMessages(int id)
    {
        return _context.Tasks?
            .Include(x=>x.Messages)
            .FirstOrDefault(x => x.Id == id);
    }
}
