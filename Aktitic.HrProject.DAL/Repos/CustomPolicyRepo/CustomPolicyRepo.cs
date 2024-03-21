using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class CustomPolicyRepo :GenericRepo<CustomPolicy>,ICustomPolicyRepo
{
    private readonly HrSystemDbContext _context;

    public CustomPolicyRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}
