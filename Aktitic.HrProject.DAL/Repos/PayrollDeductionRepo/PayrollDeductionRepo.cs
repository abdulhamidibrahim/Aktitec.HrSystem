using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class PayrollDeductionRepo :GenericRepo<PayrollDeduction>,IPayrollDeductionRepo
{
    private readonly HrSystemDbContext _context;

    public PayrollDeductionRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PayrollDeduction> GlobalSearch(string? searchKey)
    {
        if (_context.PayrollDeductions != null)
        {
            var query = _context.PayrollDeductions
                .Include(x=>x.Employee)
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
                        x.Employee!.FullName.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.PayrollDeductions!.AsQueryable();
    }

    public IQueryable<PayrollDeduction> GetWithEmployees(int id)
    {
        
        return _context.PayrollDeductions!
            .Where(x => x.Id == id)
            .Include(x => x.Employee);
    }

    public async Task<IQueryable<PayrollDeduction>> GetAllWithEmployees()
    {
        return await Task.FromResult(_context.PayrollDeductions!
            .Include(x => x.Employee));
    }
}
