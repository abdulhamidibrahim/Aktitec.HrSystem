using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class SchedulingRepo :GenericRepo<Scheduling>,ISchedulingRepo
{
    private readonly HrSystemDbContext _context;

    public SchedulingRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    // public async Task<IEnumerable<Scheduling>> GetSchedulingByUserId(int userId, DateOnly date)
    // {
    //     
    //     return await _context.Schedulings.Where(x => x.EmployeeId == userId && x.Date == date).ToListAsync();
    // }
    //
    // public async Task<IEnumerable<Scheduling>> GetSchedulingByUserId(int userId)
    // {
    //     return await _context.Schedulings.Where(x => x.EmployeeId == userId).ToListAsync();
    // }

    public List<Scheduling> GetSchedulingWithEmployees()
    {   
        //get all employees with pagination for 7 days from now
        var startDate = DateTime.Today.Date; // Start from today
        var endDate = startDate.AddDays(7);

        var query = _context.Schedulings?
            .Include(s => s.Employee)
            .Where(s =>
                (!s.Date.HasValue || 
                 (s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day >= startDate.Year * 10000 + startDate.Month * 100 + startDate.Day 
                  && s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day <= endDate.Year * 10000 + endDate.Month * 100 + endDate.Day)))
            .OrderBy(s => s.Date)
            .ThenBy(s => s.StartTime)
            .AsQueryable();
            
        return query.ToList();
    }

    
    public IQueryable<Scheduling> GlobalSearch(string? searchKey)
    {
        if (_context.Schedulings != null)
        {
            var query = _context.Schedulings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Date!.ToString().ToLower().Contains(searchKey) ||
                        x.Employee.FullName!.ToLower().Contains(searchKey) ||
                        x.Department.Name.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Schedulings!.AsQueryable();
    }
}
