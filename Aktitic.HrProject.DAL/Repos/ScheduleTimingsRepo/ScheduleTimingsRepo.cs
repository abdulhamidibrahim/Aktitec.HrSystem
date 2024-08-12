using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class ScheduleTimingsRepo :GenericRepo<ScheduleTiming>,IScheduleTimingsRepo
{
    private readonly HrSystemDbContext _context;

    public ScheduleTimingsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<ScheduleTiming> GlobalSearch(string? searchKey)
    {
        if (_context.ScheduleTimings != null)
        {
            var query = _context.ScheduleTimings
                .Include(x=>x.Employee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.ScheduleDate1 == searchDate ||
                            x.ScheduleDate2 == searchDate ||
                            x.ScheduleDate3 == searchDate );
                    return query;
                }
                
                
                if(query.Any(x => x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.SelectTime1!.ToLower().Contains(searchKey) ||
                        x.SelectTime2!.ToLower().Contains(searchKey) ||
                        x.SelectTime3!.ToLower().Contains(searchKey) );
                       
                
                return query;
            }
           
        }

        return _context.ScheduleTimings!.AsQueryable();
    }

    public async Task<IEnumerable<ScheduleTiming>> GetAllScheduleTimings()
    {
        return await _context.ScheduleTimings!
            .Include(x=>x.Employee)
            .Include(x=>x.Job)
            .ToListAsync();
    }
}
