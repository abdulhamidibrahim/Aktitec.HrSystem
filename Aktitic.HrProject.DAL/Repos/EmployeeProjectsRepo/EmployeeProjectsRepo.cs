using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class EmployeeProjectsRepo :GenericRepo<EmployeeProjects>,IEmployeeProjectsRepo
{
    private readonly HrSystemDbContext _context;

    public EmployeeProjectsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}
