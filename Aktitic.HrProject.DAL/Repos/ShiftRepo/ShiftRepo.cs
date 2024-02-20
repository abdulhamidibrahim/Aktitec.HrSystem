using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class ShiftRepo :GenericRepo<Shift>,IShiftRepo
{
    private readonly HrManagementDbContext _context;

    public ShiftRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
