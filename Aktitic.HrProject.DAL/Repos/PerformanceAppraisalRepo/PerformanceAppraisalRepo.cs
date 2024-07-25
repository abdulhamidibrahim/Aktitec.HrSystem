using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class PerformanceAppraisalRepo :GenericRepo<PerformanceAppraisal>,IPerformanceAppraisalRepo
{
    private readonly HrSystemDbContext _context;

    public PerformanceAppraisalRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PerformanceAppraisal> GlobalSearch(string? searchKey)
    {
        if (_context.PerformanceAppraisals != null)
        {
            var query = _context.PerformanceAppraisals
                .Include(x=>x.Employee)
                .ThenInclude(x=>x.Department)
                .ThenInclude(x=>x.Designations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }

                query = query
                    .Where(x =>
                        x.Employee.FullName!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) ||
                        x.Employee.Department.Name!.ToLower().Contains(searchKey) 
                        );
                        // x.Employee!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.PerformanceAppraisals!.AsQueryable();
    }
    
    public IQueryable<PerformanceAppraisal> GetAllWithEmployees()
    {
        
        return _context.PerformanceAppraisals!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .ThenInclude(x=>x.Designations);
    }
    
    public IQueryable<PerformanceAppraisal> GetWithEmployees(int id)
    {
        
        return _context.PerformanceAppraisals!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .ThenInclude(x=>x.Designations);
    }
}
