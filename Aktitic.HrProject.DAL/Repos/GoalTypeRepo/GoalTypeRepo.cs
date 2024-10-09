using System.Linq;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class GoalTypeRepo :GenericRepo<GoalType>,IGoalTypeRepo
{
    private readonly HrSystemDbContext _context;

    public GoalTypeRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<GoalType> GlobalSearch(string? searchKey)
    {
        if (_context.GoalTypes != null)
        {
            var query = _context.GoalTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                //
                // if( DateOnly.TryParse(searchKey, out var searchDate))
                // {
                //     query = query
                //         .Where(x =>
                //             x.Date == searchDate );
                //     return query;
                // }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Type!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.GoalTypes!.AsQueryable();
    }
}
