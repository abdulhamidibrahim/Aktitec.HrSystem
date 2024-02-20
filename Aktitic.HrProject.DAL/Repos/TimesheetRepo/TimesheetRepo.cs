using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TimesheetRepo :GenericRepo<Timesheet>,ITimesheetRepo
{
    private readonly HrManagementDbContext _context;

    public TimesheetRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
