using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TimesheetRepo :GenericRepo<TimeSheet>,ITimesheetRepo
{
    private readonly HrSystemDbContext _context;

    public TimesheetRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<TimeSheet> GlobalSearch(string? searchKey)
    {
        if (_context.Timesheets != null)
        {
            var query = _context.Timesheets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();

                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                               x.Date == searchDate
                            || x.Date == searchDate);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Date!.ToString().ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Hours!.ToString().ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Timesheets!.AsQueryable();
    }

    public Task<List<TimeSheet>> GetTimeSheetWithEmployeeAndProject()
    {
        return _context.Timesheets
            .Include(x => x.Employee)
            .Include(x => x.Project).ToListAsync();
    }
}
