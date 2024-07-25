using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class PayrollAdditionRepo :GenericRepo<PayrollAddition>,IPayrollAdditionRepo
{
    private readonly HrSystemDbContext _context;

    public PayrollAdditionRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PayrollAddition> GlobalSearch(string? searchKey)
    {
        if (_context.PayrollAdditions != null)
        {
            var query = _context.PayrollAdditions
                .Include(x=>x.Employee)
                .Include(x=>x.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.UnitAmount!.ToString().Contains(searchKey) ||
                        x.UnitCalculation!.ToString().Contains(searchKey) ||
                        x.Assignee!.ToLower().Contains(searchKey) ||
                        x.Category!.CategoryName.ToLower().Contains(searchKey) ||
                        x.Employee!.FullName.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.PayrollAdditions!.AsQueryable();
    }

    public IQueryable<PayrollAddition> GetWithEmployees(int id)
    {
        
        return _context.PayrollAdditions!
            .Where(x => x.Id == id)
            .Include(x => x.Employee)
            .Include(x=>x.Category);
    }

    public async Task<IQueryable<PayrollAddition>> GetAllWithEmployees()
    {
        return await Task.FromResult(_context.PayrollAdditions!
            .Include(x => x.Employee)
            .Include(x=>x.Category));
    }
}
