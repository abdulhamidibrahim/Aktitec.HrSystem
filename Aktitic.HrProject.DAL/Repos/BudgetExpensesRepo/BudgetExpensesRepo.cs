using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class BudgetExpensesRepo :GenericRepo<BudgetExpenses>,IBudgetExpensesRepo
{
    private readonly HrSystemDbContext _context;

    public BudgetExpensesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<BudgetExpenses> GlobalSearch(string? searchKey)
    {
        if (_context.BudgetsExpenses!= null)
        {
            var query = 
                _context.BudgetsExpenses
                    .Include(x=>x.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Currency!.ToLower().Contains(searchKey) ||
                        x.Subcategory!.ToLower().Contains(searchKey) ||
                        x.Amount!.ToString().Contains(searchKey) ||
                        x.Note!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.BudgetsExpenses!.AsQueryable();
    }

    public BudgetExpenses? GetWithCategory(int id)
    {
        if (_context.BudgetsExpenses != null)
            return _context.BudgetsExpenses
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        return new BudgetExpenses();
    }

    public Task<List<BudgetExpenses>> GetAllWithCategoryAsync()
    {
        if (_context.BudgetsExpenses != null)
            return _context.BudgetsExpenses
                .Include(x => x.Category)
                .ToListAsync();
        return Task.FromResult(new List<BudgetExpenses>());
    }
}
