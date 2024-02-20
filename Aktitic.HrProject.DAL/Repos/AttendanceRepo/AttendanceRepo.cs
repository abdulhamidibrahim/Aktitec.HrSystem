using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class AttendanceRepo :GenericRepo<Attendance>,IAttendanceRepo
{
    private readonly HrManagementDbContext _context;

    public AttendanceRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
