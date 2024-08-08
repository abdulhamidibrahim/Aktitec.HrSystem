using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class ShortlistsRepo(HrSystemDbContext context) : GenericRepo<Shortlist>(context), IShortlistsRepo
{
    private readonly HrSystemDbContext _context = context;

    public IQueryable<Shortlist> GlobalSearch(string? searchKey)
    {
        if (_context.Shortlists != null)
        {
            var query = _context.Shortlists
                .Include(x=>x.Employee)
                .Include(x=>x.Job)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if(query.Any(x => x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey));
                
                if(query.Any(x => x.Job.JobTitle.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Job.JobTitle.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Status!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Shortlists!.AsQueryable();
    }
    
}
