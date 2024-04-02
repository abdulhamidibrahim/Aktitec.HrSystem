using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class CustomPolicyRepo :GenericRepo<CustomPolicy>,ICustomPolicyRepo
{
    private readonly HrSystemDbContext _context;

    public CustomPolicyRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public List<CustomPolicy>? GetByType(string type)
    {
        if (_context.CustomPolicies != null) 
            return _context.CustomPolicies
                .Include(x=>x.Employee)
                .Where(x => x.Type == type).ToList();
        return new List<CustomPolicy>();
    }
}
