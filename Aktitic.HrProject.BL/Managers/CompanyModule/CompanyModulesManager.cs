using Aktitic.HrProject.BL.Dtos.CompanyModules;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;

namespace Aktitic.HrTaskList.BL;

public class CompanyModulesManager(
    IUnitOfWork unitOfWork,
    UserUtility userUtility,
    IMapper mapper) : ICompanyModulesManager
{
    public void Add(CompanyModuleDto companyModuleDto)
    {
        var companyId = userUtility.GetCurrentCompany();
        var compId = Convert.ToInt32(companyId);
        var companyModule = new CompanyModule()
        {
            CompanyId = compId,
            AppModuleId = companyModuleDto.AppModulesId
        };
        unitOfWork.CompanyModules.Add(companyModule);
        unitOfWork.SaveChangesAsync();
    }

    public async Task<List<CompanyModuleDto>> GetAll()
        {
            var companyId = userUtility.GetCurrentCompany();
            var compId = Convert.ToInt32(companyId);
            var appModules = await unitOfWork.CompanyModules.GetCompanyModules(compId);
            var result = new List<CompanyModuleDto>();
            if (appModules != null)
            {
                foreach (var appModule in appModules)
                {
                    var appModuleDto = new CompanyModuleDto()
                    {
                        CompanyId = appModule.CompanyId,
                        AppModulesId = appModule.AppModuleId,
                    };
                    result.Add(appModuleDto);
                }
            }
            return result;
        }
} 
