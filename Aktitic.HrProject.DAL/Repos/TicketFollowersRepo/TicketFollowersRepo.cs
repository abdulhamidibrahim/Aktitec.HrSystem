using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TicketFollowersRepo :GenericRepo<TicketFollowers>,ITicketFollowersRepo
{
    private readonly HrSystemDbContext _context;

    public TicketFollowersRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}
