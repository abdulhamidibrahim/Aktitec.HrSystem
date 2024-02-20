using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class OvertimeRepo :GenericRepo<Overtime>,IOvertimeRepo
{
    private readonly HrManagementDbContext _context;

    public OvertimeRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
