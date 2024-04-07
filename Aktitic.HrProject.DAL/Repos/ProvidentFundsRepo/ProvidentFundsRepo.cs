using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ProvidentFundsRepo :GenericRepo<ProvidentFunds>,IProvidentFundsRepo
{
    private readonly HrSystemDbContext _context;

    public ProvidentFundsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<ProvidentFunds> GlobalSearch(string? searchKey)
    {
        if (_context.ProvidentFunds != null)
        {
            var query = _context.ProvidentFunds.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.ProvidentType!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.EmployeeShareAmount!.ToString().Contains(searchKey) ||
                        x.OrganizationShareAmount!.ToString().Contains(searchKey) ||
                        x.EmployeeSharePercentage!.ToString().Contains(searchKey) ||
                        x.OrganizationSharePercentage!.ToString().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.ProvidentFunds!.AsQueryable();
    }

    public ProvidentFunds? GetWithEmployees(int id)
    {
        if (_context.ProvidentFunds != null)
            return _context.ProvidentFunds
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.Id == id);
        return null;
    }

    public Task<List<ProvidentFunds>> GetAllWithEmployeesAsync()
    {
        if (_context.ProvidentFunds != null)
            return _context.ProvidentFunds
                .Include(x => x.Employee)
                .ToListAsync();
        return Task.FromResult(new List<ProvidentFunds>());
    }
}
