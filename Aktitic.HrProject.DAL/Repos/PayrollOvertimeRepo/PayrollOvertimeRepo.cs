using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class PayrollOvertimeRepo :GenericRepo<PayrollOvertime>,IPayrollOvertimeRepo
{
    private readonly HrSystemDbContext _context;

    public PayrollOvertimeRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PayrollOvertime> GlobalSearch(string? searchKey)
    {
        if (_context.PayrollOvertimes != null)
        {
            var query = _context.PayrollOvertimes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Rate!.ToString().Contains(searchKey) ||
                        x.RateType!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.PayrollOvertimes!.AsQueryable();
    }
}
