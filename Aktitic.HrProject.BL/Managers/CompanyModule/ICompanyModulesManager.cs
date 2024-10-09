using Aktitic.HrProject.BL.Dtos.AppModules;
using Aktitic.HrProject.BL.Dtos.CompanyModules;

namespace Aktitic.HrTaskList.BL;

public interface ICompanyModulesManager
{
    void Add(CompanyModuleDto companyModuleAddDto);
    // public Task<CompanyModuleDto>? Get(int id);
    public Task<List<CompanyModuleDto>> GetAll();
}