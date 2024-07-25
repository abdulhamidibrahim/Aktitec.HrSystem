using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskBoardRepo :GenericRepo<TaskBoard>,ITaskBoardRepo
{
    private readonly HrSystemDbContext _context;

    public TaskBoardRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<TaskBoard>> GetByProjectId(int projectId)
    {
        if (_context.TaskBoards != null) return _context.TaskBoards
            .Where(x => x.ProjectId == projectId)
            .Include(x=> x.TaskLists)
            .ThenInclude(x=>x.Task)
            .ToListAsync();
        return Task.FromResult(new List<TaskBoard>());
    }

    public Task<List<TaskBoard>> GetAllWithTaskLists()
    {
        if (_context.TaskBoards != null) return _context.TaskBoards
            .Include(x => x.TaskLists)
            .ThenInclude(x=>x.Task)
            .ToListAsync();
        return Task.FromResult(new List<TaskBoard>());
    }

    public Task<TaskBoard?> GetWithTaskLists(int id)
    {
        
        if (_context.TaskBoards != null) return _context.TaskBoards
            .Include(x => x.TaskLists)
            .ThenInclude(x=>x.Task)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return Task.FromResult(new TaskBoard());
    }
}
