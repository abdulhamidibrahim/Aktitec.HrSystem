using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class LeavesRepo :GenericRepo<Leaves>,ILeavesRepo
{
    private readonly HrSystemDbContext _context;

    public LeavesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    
    public IQueryable<Leaves> GlobalSearch(string? searchKey)
    {
        if (_context.Leaves != null)
        {
            var query = _context.Leaves.Include(e=>e.Employee).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.FromDate == searchDate ||
                            x.ToDate == searchDate);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Type!.ToLower().Contains(searchKey) ||
                        x.Reason!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Leaves!.AsQueryable();
    }

    public List<Leaves> GetLeavesWithEmployee()
    {
        return _context.Leaves
            .Include(e => e.Employee)
            .Include(e => e.ApprovedByNavigation).ToList();
    }
}
