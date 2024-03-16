using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TicketFollowersRepo :GenericRepo<TicketFollowers>,ITicketFollowersRepo
{
    private readonly HrManagementDbContext _context;

    public TicketFollowersRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
