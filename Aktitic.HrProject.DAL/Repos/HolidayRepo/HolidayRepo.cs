using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class HolidayRepo :GenericRepo<Holiday>,IHolidayRepo
{
    private readonly HrManagementDbContext _context;

    public HolidayRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
