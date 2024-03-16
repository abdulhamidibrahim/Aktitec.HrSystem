using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaskBoardRepo :GenericRepo<TaskBoard>,ITaskBoardRepo
{
    private readonly HrManagementDbContext _context;

    public TaskBoardRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
