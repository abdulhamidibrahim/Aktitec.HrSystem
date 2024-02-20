using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class DesignationRepo :GenericRepo<Designation>,IDesignationRepo
{
    private readonly HrManagementDbContext _context;

    public DesignationRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
