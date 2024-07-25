using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ExpensesOfBudgetRepo :GenericRepo<ExpensesOfBudget>,IExpensesOfBudgetRepo
{
    private readonly HrSystemDbContext _context;

    public ExpensesOfBudgetRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Estimate> GlobalSearch(string? searchKey)
    {
        if (_context.Estimates != null)
        {
            var query = 
                _context.Estimates
                .Include(x=>x.Items)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.EstimateDate == searchDate ||
                            x.ExpiryDate == searchDate);
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.OtherInformation!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.BillingAddress!.ToLower().Contains(searchKey) ||
                        x.ClientAddress!.ToLower().Contains(searchKey) ||
                        x.EstimateNumber!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Estimates!.AsQueryable();
    }

    public Estimate GetEstimateWithItems(int id)
    {
        return _context.Estimates!
            .Include(x => x.Items)
            .FirstOrDefault(x => x.Id == id);
    }

    public Task<List<Estimate>> GetAllEstimateWithItems()
    {
        return
            _context.Estimates!
                .Include(x => x.Items)
                .ToListAsync();
    }
}
