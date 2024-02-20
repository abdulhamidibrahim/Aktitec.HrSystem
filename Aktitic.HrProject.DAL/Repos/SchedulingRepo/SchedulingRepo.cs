using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class SchedulingRepo :GenericRepo<Scheduling>,ISchedulingRepo
{
    private readonly HrManagementDbContext _context;

    public SchedulingRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
