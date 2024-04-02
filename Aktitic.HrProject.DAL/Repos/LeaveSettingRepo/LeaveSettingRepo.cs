using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class LeaveSettingRepo :GenericRepo<LeaveSettings>,ILeaveSettingRepo
{
    private readonly HrSystemDbContext _context;

    public LeaveSettingRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Leaves>? GetLeavesWithEmployee()
    {
        return _context.Leaves?
            .Include(x => x.Employee)
            .Include(x => x.ApprovedByNavigation)
            .ToList();
    }
}
