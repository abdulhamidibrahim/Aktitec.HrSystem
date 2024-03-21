using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskListRepo :GenericRepo<TaskList>,ITaskListRepo
{
    private readonly HrSystemDbContext _context;

    public TaskListRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}
