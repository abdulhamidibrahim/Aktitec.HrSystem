using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class AptitudeResultsRepo :GenericRepo<AptitudeResult>,IAptitudeResultsRepo
{
    private readonly HrSystemDbContext _context;

    public AptitudeResultsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<AptitudeResult> GlobalSearch(string? searchKey)
    {
        if (_context.AptitudeResults != null)
        {
            var query = _context.AptitudeResults
                .Include(x=>x.Employee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
               
                
                
                if(query.Any(x => x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.CategoryWiseMark!.ToLower().Contains(searchKey) ||
                        x.TotalMark!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.AptitudeResults!.AsQueryable();
    }

    public async Task<IEnumerable<AptitudeResult>> GetAllAptitudeResults()
    {
        return await _context.AptitudeResults!
            .Include(x=>x.Employee)
            .Include(x=>x.Job)
            .ToListAsync();
    }
}
