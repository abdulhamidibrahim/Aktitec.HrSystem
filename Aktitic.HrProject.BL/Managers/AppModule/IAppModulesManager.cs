using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL.Dtos.AppModules;

namespace Aktitic.HrTaskList.BL;

public interface IAppModulesManager
{
    
    public Task<AppModuleDto>? Get(int id);
    public Task<List<AppModuleDto>> GetAll();
    Task<int?> AssignComapnyModules(List<AppModuleDto> appModuleDto, int companyId);
    Task<List<AppModuleDto>?> GetCompanyModules(int companyId);
    public List<AppModuleDto> GetRole(int roleId);
    public Task<int> UpdateRole(CompanyRolesDto rolePermissions, int companyId, int roleId);
    Task<int> CreateRole(CompanyRolesDto rolePermissions, int companyId);
    Task<List<CompanyRoleDto>?> GetCompanyRoles(int companyId);
    Task<int?> UpdateCompanyModules(AppModuleDto appModuleDto, int companyId);

    Task<ApiRespone<string>> DeleteRole(int roleId);
}