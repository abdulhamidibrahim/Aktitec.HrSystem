using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICompanyRolesRepo :IGenericRepo<CompanyRole>
{
    Task<IEnumerable<CompanyRole>> GetCompanyRoles(int companyId);   
    Task<CompanyRole> GetRole(int roleId);   
    Task<int> CreateRole(CompanyRole companyRole);
    
    public Task<ApiRespone<string>> DeleteRole(int roleId);
    
    void AssignCompanyRoles(List<CompanyRole> companyRole);
}
