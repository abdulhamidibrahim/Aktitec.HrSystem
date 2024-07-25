using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.GoalListRepo;

public class GoalListRepo :GenericRepo<GoalList>,IGoalListRepo
{
    private readonly HrSystemDbContext _context;

    public GoalListRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<GoalList> GlobalSearch(string? searchKey)
    {
        if (_context.GoalLists != null)
        {
            var query = _context.GoalLists
                .Include(x=>x.GoalType).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate == searchDate ||
                            x.EndDate == searchDate );
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.GoalType!.Type.ToLower().Contains(searchKey) ||
                        x.Subject!.ToLower().Contains(searchKey) ||
                        x.TargetAchievement!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.GoalLists!.AsQueryable();
    }

    public IQueryable<GoalList> GetWithGoalType(int id)
    {
        
        return _context.GoalLists!
            .Where(x => x.Id == id)
            .Include(x => x.GoalType).AsQueryable();
    }

    public IQueryable<GoalList> GetAllWithGoalType()
    {
        return _context.GoalLists!
            .Include(x => x.GoalType).AsQueryable();
    }
}
