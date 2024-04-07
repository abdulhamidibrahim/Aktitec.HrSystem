using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class BudgetRevenuesRepo :GenericRepo<BudgetRevenue>,IBudgetRevenuesRepo
{
    private readonly HrSystemDbContext _context;

    public BudgetRevenuesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<BudgetRevenue> GlobalSearch(string? searchKey)
    {
        if (_context.BudgetsRevenues!= null)
        {
            var query = 
                _context.BudgetsRevenues
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

        return _context.BudgetsRevenues!.AsQueryable();
    }

    public BudgetRevenue? GetWithCategory(int id)
    {
        if (_context.BudgetsRevenues != null)
            return _context.BudgetsRevenues
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        return null;
    }

    public Task<List<BudgetRevenue>> GetAllWithCategoryAsync()
    {
        if (_context.BudgetsRevenues != null)
            return _context.BudgetsRevenues
                .Include(x => x.Category)
                .ToListAsync();
        return Task.FromResult(new List<BudgetRevenue>());
    }
}
