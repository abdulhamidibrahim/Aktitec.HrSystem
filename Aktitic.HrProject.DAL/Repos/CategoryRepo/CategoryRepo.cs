using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class BudgetRepo :GenericRepo<Budget>,IBudgetRepo
{
    private readonly HrSystemDbContext _context;

    public BudgetRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Budget> GlobalSearch(string? searchKey)
    {
        if (_context.Budgets!= null)
        {
            var query = 
                _context.Budgets
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                searchKey = searchKey.Trim().ToLower();
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate == searchDate ||
                            x.EndDate == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Title!.ToLower().Contains(searchKey) ||
                        x.OverallRevenue!.ToString().Contains(searchKey) ||
                        x.OverallExpense!.ToString().Contains(searchKey) ||
                        x.ExpectedProfit!.ToString().Contains(searchKey) ||
                        x.Tax!.ToString().Contains(searchKey) ||
                        x.BudgetAmount!.ToString().Contains(searchKey) ||
                        x.Type!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Budgets!.AsQueryable();
    }

    public Budget? GetWithDetails(int id)
    {
        if (_context.Budgets != null)
            return _context.Budgets
                .Include(x => x.Expenses)
                .Include(x => x.Revenues)
                .FirstOrDefault(x => x.Id == id);
        return null;
    }

    public Task<List<Budget>> GetAllWithDetails()
    {
        if (_context.Budgets != null)
            return _context.Budgets
                .Include(x => x.Expenses)
                .Include(x => x.Revenues).ToListAsync();
        return Task.FromResult(new List<Budget>());
    }
}
