using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class LeavesRepo :GenericRepo<Leaves>,ILeavesRepo
{
    private readonly HrManagementDbContext _context;

    public LeavesRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
