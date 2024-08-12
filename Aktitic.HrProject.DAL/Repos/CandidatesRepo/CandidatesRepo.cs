using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class CandidatesRepo :GenericRepo<Candidate>,ICandidatesRepo
{
    private readonly HrSystemDbContext _context;

    public CandidatesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Candidate> GlobalSearch(string? searchKey)
    {
        if (_context.Candidates != null)
        {
            var query = _context.Candidates
                .Include(x=>x.Employee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.CreatedAt == searchDate  );
                    return query;
                }
                
                
                if(query.Any(x => x.Employee != null && x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>  x.Employee != null && x.Employee.FullName != null  && x.Employee.FullName.ToLower().Contains(searchKey));


                query = query
                    .Where(x =>
                        x.FirstName!.ToLower().Contains(searchKey) ||
                        x.LastName!.ToLower().Contains(searchKey) ||
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.Phone!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Candidates!.AsQueryable();
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidates()
    {
        return await _context.Candidates!
            .Include(x=>x.Employee)
            .ToListAsync();
    }
}
