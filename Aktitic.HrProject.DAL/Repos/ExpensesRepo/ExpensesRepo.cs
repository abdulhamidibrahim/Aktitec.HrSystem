using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ExpensesRepo :GenericRepo<Expenses>,IExpensesRepo
{
    private readonly HrSystemDbContext _context;

    public ExpensesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Expenses> GlobalSearch(string? searchKey)
    {
        if (_context.Expenses != null)
        {
            var query = 
                _context.Expenses
                    .Include(x=>x.PurchasedBy)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.PurchaseDate == searchDate);
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.ItemName!.ToLower().Contains(searchKey) ||
                        x.PurchaseFrom!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.PurchasedBy!.FullName.ToLower().Contains(searchKey) ||
                        x.PaidBy!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Expenses!.AsQueryable();
    }

    public Expenses GetEstimateWithEmployee(int id)
    {
        return _context.Expenses
            .Include(x => x.PurchasedBy)
            .FirstOrDefault(x => x.Id == id);
    }

    public Task<List<Expenses>> GetAllEstimateWithEmployees()
    {
        return _context.Expenses
            .Include(x => x.PurchasedBy)
            .ToListAsync();
    }
}
