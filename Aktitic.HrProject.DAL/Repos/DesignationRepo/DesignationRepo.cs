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
    
}
