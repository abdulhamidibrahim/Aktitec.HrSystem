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
    
    public IQueryable<Department> GlobalSearch(string? searchKey)
    {
        if (_context.Departments != null)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Departments!.AsQueryable();
    }
}

    
