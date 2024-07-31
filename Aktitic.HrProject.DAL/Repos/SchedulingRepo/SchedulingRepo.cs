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

    // public List<Scheduling> GetSchedulingWithEmployees(DateTime startDate)
    // {   
    //     //get all employees with pagination for 7 days from now
    //      // startDate = DateTime.Today.Date; // Start from today
    //     var endDate = startDate.AddDays(7);
    //
    //     var query = _context.Schedulings?
    //         .Include(s => s.Employee)
    //         .Where(s =>
    //             (!s.Date.HasValue || 
    //              (s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day >= startDate.Year * 10000 + startDate.Month * 100 + startDate.Day 
    //               && s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day <= endDate.Year * 10000 + endDate.Month * 100 + endDate.Day)))
    //         .OrderBy(s => s.Date)
    //         .ThenBy(s => s.StartTime)
    //         .AsQueryable();
    //
    //     if (query != null) return query.ToList();
    //     return new List<Scheduling>();
    // }

    // public List<Scheduling> GetSchedulingWithEmployees()
    // {
    //     var startDate = DateOnly.FromDateTime(DateTime.Today.Date); // Start from today
    //     var endDate = startDate.AddDays(7);
    //
    //     var query = _context.Schedulings?
    //         .Include(s => s.Employee)
    //         .Where(s =>
    //             (!s.Date.HasValue || 
    //              (s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day >= startDate.Year * 10000 + startDate.Month * 100 + startDate.Day 
    //               && s.Date.Value.Year * 10000 + s.Date.Value.Month * 100 + s.Date.Value.Day <= endDate.Year * 10000 + endDate.Month * 100 + endDate.Day)))
    //         .OrderBy(s => s.Date)
    //         .ThenBy(s => s.StartTime)
    //         .AsQueryable();
    //     if (query != null) return query.ToList();
    //     return new List<Scheduling>();
    // }
    //
    
    public List<Scheduling> GetSchedulingWithEmployees()
    {   
        var startDate = DateOnly.FromDateTime(DateTime.Today.Date); // Start from today
        var endDate = startDate.AddDays(7);

        var query = _context.Schedulings?
            .Include(s => s.Employee)
            .Where(s =>
                (!s.Date.HasValue || 
                 (s.Date.Value >= startDate && s.Date.Value <= endDate)))
            .OrderBy(s => s.Date)
            .ThenBy(s => s.StartTime);

        if (query != null) return query.ToList();
        return new List<Scheduling>();
    }
    
    public List<Scheduling> GetSchedulingWithEmployees(DateOnly? startDate = null)
    {   
        if (!startDate.HasValue)
        {
            startDate = DateOnly.FromDateTime(DateTime.Today.Date); // Start from today
        }
    
        var endDate = startDate.Value.AddDays(6);

        var query = _context.Schedulings?
            .Include(s => s.Employee)
            .Where(s =>
                (!s.Date.HasValue || 
                 (s.Date.Value >= startDate && s.Date.Value <= endDate)))
            .OrderBy(s => s.Date)
            .ThenBy(s => s.StartTime)
            .AsQueryable();

        if (query != null) return query.ToList();
        return new List<Scheduling>();
    }


    

    public IQueryable<Scheduling> GlobalSearch(string? searchKey)
    {
        if (_context.Schedulings != null)
        {
            var query = _context.Schedulings
                // .Include(x=>x.ApprovedByNavigation)
                .Include(x=>x.Employee)
                .ThenInclude(x=>x.Department)
                // .Select(x=>x.Employee != null ? new Scheduling
                // {
                //     Id = x.Id,
                //     Date = x.Date,
                //     Employee = new Employee
                //     {
                //         Id = x.Employee.Id,
                //         FullName = x.Employee.FullName,
                //         Department = new Department
                //         {
                //             Id = x.Employee.Department.Id,
                //             FileName = x.Employee.Department.FileName
                //         }
                //     }
                // } : new Scheduling
                // {
                //     Id = x.Id,
                //     Date = x.Date
                // })
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Employee!.Id!.ToString().Contains(searchKey) ||
                        x.Employee!.FullName!.ToLower().Contains(searchKey) ||
                        x.Department!.Id!.ToString().Contains(searchKey) ||
                        x.Department!.Name.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Schedulings!.AsQueryable();
    }
}
