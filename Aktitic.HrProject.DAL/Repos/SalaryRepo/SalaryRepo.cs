using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class SalaryRepo :GenericRepo<Salary>,ISalaryRepo
{
    private readonly HrSystemDbContext _context;

    public SalaryRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Salary> GlobalSearch(string? searchKey)
    {
        if (_context.Salaries != null)
        {
            var query = _context.Salaries
                .Include(x=>x.Employee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.NetSalary!.ToString().Contains(searchKey) ||
                        x.BasicEarnings!.ToString().Contains(searchKey) ||
                        x.Tds!.ToString().Contains(searchKey) ||
                        x.Da!.ToString().Contains(searchKey) ||
                        x.Esi!.ToString().Contains(searchKey) ||
                        x.Hra!.ToString().Contains(searchKey) ||
                        x.Pf!.ToString().Contains(searchKey) ||
                        x.Conveyance!.ToString().Contains(searchKey) ||
                        x.Leave!.ToString().Contains(searchKey) ||
                        x.Allowance!.ToString().Contains(searchKey) ||
                        x.ProfTax!.ToString().Contains(searchKey) ||
                        x.MedicalAllowance!.ToString().Contains(searchKey) ||
                        x.LabourWelfare!.ToString().Contains(searchKey) ||
                        x.Fund!.ToString().Contains(searchKey) ||
                        x.Others1!.ToString().Contains(searchKey) ||
                        x.Others2!.ToString().Contains(searchKey) ||
                        x.PayslipId!.ToString().Contains(searchKey));
                       
                        
                return query;
            }
        }

        return _context.Salaries!.AsQueryable();
    }

    public IQueryable<Salary> GetWithEmployee(int id)
    {
        
        return _context.Salaries!
            .Include(x=>x.Employee)
            .Where(x => x.Id == id);
    }

    public async Task<IQueryable<Salary>> GetAllWithEmployee()
    {
        return await Task.FromResult(_context.Salaries!
            .Include(x => x.Employee)
            .AsQueryable());
    }
}
