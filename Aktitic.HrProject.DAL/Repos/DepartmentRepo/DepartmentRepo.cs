using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class DepartmentRepo :GenericRepo<Department>,IDepartmentRepo
{
    private readonly HrManagementDbContext _context;

    public DepartmentRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
}

    
