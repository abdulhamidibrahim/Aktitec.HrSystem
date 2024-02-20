using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class EmployeeRepo :GenericRepo<Employee>,IEmployeeRepo
{
    private readonly HrManagementDbContext _context;

    public EmployeeRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}
