using System.Linq.Dynamic.Core;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class CompanyRepo :GenericRepo<Company>,ICompanyRepo
{
    private readonly HrSystemDbContext _context;

    public CompanyRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Company> GlobalSearch(string? searchKey)
    {
        
        if (_context.Companies != null)
        {
            var query = _context.Companies
                .Include(x=>x.Manager)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateTime.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.CreatedAt == searchDate  );
                    return query;
                }

                if(query.Any(x => x.Manager.FullName != null && x.Manager.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Manager.FullName.ToLower().Contains(searchKey));
                
                query = query
                    .Where(x =>
                           (x.CompanyName.ToLower().Contains(searchKey) ||
                            x.Email.ToLower().Contains(searchKey) ||
                            x.Address.ToLower().Contains(searchKey) ||
                            x.Phone.ToLower().Contains(searchKey) ||
                            x.Website.ToLower().Contains(searchKey) ||
                            x.Fax.ToLower().Contains(searchKey) ||
                            x.Country.ToLower().Contains(searchKey) ||
                            x.City.ToLower().Contains(searchKey) ||
                            x.State.ToLower().Contains(searchKey) ||
                            x.Postal.ToLower().Contains(searchKey) ||
                            x.Contact.ToLower().Contains(searchKey) ||
                            x.CreatedBy.ToLower().Contains(searchKey) ) );
                
                return query;
            }
               
        }

        return _context.Companies.AsQueryable();
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        if (_context.Companies != null)
            return await _context.Companies
                .Include(x => x.Manager)
                .AsQueryable()
                .ToListAsync();

        return await Task.FromResult(Enumerable.Empty<Company>());
    }

    public Company GetCompany(int companyId)
    {

        var company = _context.Companies
            .Include(x=>x.Manager)
            .FirstOrDefault(x => x.Id == companyId);
        return company;

    }


    public async Task<int> Create(Company company)
    {
        var createdCompany =await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
        return (createdCompany.Entity.Id);
    }

  
}
