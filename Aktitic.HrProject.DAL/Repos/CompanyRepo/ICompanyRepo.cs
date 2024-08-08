using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICompanyRepo :IGenericRepo<Company>
{
    IQueryable<Company> GlobalSearch(string? searchKey);
    public Task<IEnumerable<Company>> GetAllCompanies();
    public Company GetCompany(int companyId);
    public Task<int> Create(Company company);
    
}