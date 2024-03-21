using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskBoardRepo :GenericRepo<TaskBoard>,ITaskBoardRepo
{
    private readonly HrSystemDbContext _context;

    public TaskBoardRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}
