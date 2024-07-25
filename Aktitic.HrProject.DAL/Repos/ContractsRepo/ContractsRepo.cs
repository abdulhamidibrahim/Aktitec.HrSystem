using System.Linq.Dynamic.Core;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ContractsRepo :GenericRepo<Contract>,IContractsRepo
{
    private readonly HrSystemDbContext _context;

    public ContractsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Contract> GlobalSearch(string? searchKey)
    {
        
        if (_context.Contracts != null)
        {
            var query = _context.Contracts
                .Include(x=>x.Employee)
                .Include(x=>x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateOnly.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.ContractEndDate == searchDate ||
                            x.ContractStartDate == searchDate );
                    return query;
                }

                if(query.Any(x => x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Employee.FullName.ToLower().Contains(searchKey));
                if(query.Any(x => x.Department.Name != null && x.Department.Name.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Department.Name.ToLower().Contains(searchKey));
                
                query = query
                    .Where(x =>
                           (x.ContractReference.ToLower().Contains(searchKey) ||
                            x.SalaryStructureType.ToLower().Contains(searchKey) ||
                            x.JobPosition.ToLower().Contains(searchKey) ||
                            x.ContractType.ToLower().Contains(searchKey) ||
                            x.ContractSchedule.ToLower().Contains(searchKey) ||
                            x.Notes.ToLower().Contains(searchKey) ||
                            x.Wage.ToString().Contains(searchKey) ||
                            x.Status.ToString().Contains(searchKey)) );
                
                return query;
            }
               
        }

        return _context.Contracts.AsQueryable();
    }

    public async Task<IEnumerable<Contract>> GetAllWithEmployeeAndDepartment()
    {
        if (_context.Contracts != null)
            return await _context.Contracts
                .Include(x => x.Employee)
                .Include(x => x.Department).AsQueryable().ToListAsync();

        return await Task.FromResult(Enumerable.Empty<Contract>());
    }
}
