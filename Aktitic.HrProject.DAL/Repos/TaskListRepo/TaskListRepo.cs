using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskListRepo :GenericRepo<TaskList>,ITaskListRepo
{
    private readonly HrSystemDbContext _context;

    public TaskListRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<TaskList>> GetAllByTaskBoardId(int id)
    {
        if (_context.TaskLists != null)
            return await _context.TaskLists
                .Include(x => x.Task)
                .Where(x => x.TaskBoardId == id)
                .ToListAsync();
        return new List<TaskList>();
    }

    public async Task<IEnumerable<TaskList>> GetAllWithTask()
    {
        if (_context.TaskLists != null)
            return await _context.TaskLists
                .Include(x => x.Task)
                .ToListAsync();
        return new List<TaskList>();
    }
}
