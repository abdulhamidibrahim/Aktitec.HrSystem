using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class DesignationRepo :GenericRepo<Designation>,IDesignationRepo
{
    private readonly HrSystemDbContext _context;

    public DesignationRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Designation> GlobalSearch(string? searchKey)
    {
        if (_context.Designations != null)
        {
            var query = _context.Designations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Designations!.AsQueryable();
    }
    // get designations with departments
    
    public IQueryable<Designation> GetDesignationsWithDepartments()
    {
        return _context.Designations!.Include(d => d.Department).AsQueryable();
    }
   
}
