using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.AppModules;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class AppModulesManager(
    IUnitOfWork unitOfWork
    // ,UserUtility userUtility
    ) : IAppModulesManager
{
    public async Task<AppModuleDto>? Get(int id)
    {
        var appModule = await unitOfWork.AppModules.GetAppModuleById(id);
        var result = new AppModuleDto();
        if (appModule != null)
        {
            result.Id = appModule.Id;
            result.Name = appModule.Name;
            result.SubModuleDto = appModule.AppSubModules?.Select(x => new AppSubModuleDto
            {
                Id = x.Id,
                Name = x.Name,
                PageDto = x.AppPages?.Select(y => new AppPageDto
                {
                    // Id = y.Id,
                    Name = y.Name,
                    Code = y.Code,

                }).ToList(),

            }).ToList();
        }
        return result;
    }

        public async Task<List<AppModuleDto>> GetAll()
        {
            var appModules = await unitOfWork.AppModules.GetAppModules();
            var result = new List<AppModuleDto>();
            if (appModules != null)
            {
                foreach (var appModule in appModules)
                {
                    var appModuleDto = new AppModuleDto
                    {
                        Id = appModule.Id,
                        Name = appModule.Name,
                        ArabicName = appModule.ArabicName,
                        SubModuleDto = appModule.AppSubModules?.Select(x => new AppSubModuleDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            ArabicName = x.ArabicName,
                            PageDto = x.AppPages?.Select(y => new AppPageDto
                            {
                                // Id = y.Id,
                                Name = y.Name,
                                Code = y.Code,
                                ArabicName = y.ArabicName,
                            }).ToList(),

                        }).ToList()
                    };
                    result.Add(appModuleDto);
                }
            }
            return result;
        }

        public async Task<int?> AssignComapnyModules(List<AppModuleDto> appModuleDto, int companyId)
        {
            foreach (var module in appModuleDto)
            {
                var companyModule = new CompanyModule()
                {
                    CompanyId = companyId,
                    AppModuleId = module.Id,
                   
                };
                unitOfWork.CompanyModules.Add(companyModule);
            }
            
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AppModuleDto>?> GetCompanyModules(int companyId)
        {
            var companyModules = await unitOfWork.CompanyModules.GetCompanyModules(companyId);
            var result = companyModules.Select(x => new AppModuleDto
            {
                Id = x.AppModuleId,
                Name = x.AppModule.Name,
            }).ToList();
            return result;
        }

        public async Task<int> CreateRole(CompanyRolesDto rolePermissions,int companyId)
        {
            // var companyId = Convert.ToInt32(userUtility.GetCurrentCompany());
            var company = unitOfWork.Company.GetCompany(companyId);

            var companyRole = new CompanyRole()
            {
                CompanyId = companyId,
                Name = rolePermissions.Name,
                Company = company,
                RolePermissions = rolePermissions.RolePermissions.Select(x => new RolePermissions
                {
                    PageCode = x.AppPageCode,
                    Read = x.CanRead,
                    Edit = x.CanEdit,
                    Add = x.CanAdd,
                    Delete = x.CanDelete,
                    Import = x.CanImport,
                    Export = x.CanExport,
                }).ToList(),
                    
            };
             var entityId = await unitOfWork.CompanyRoles.CreateRole(companyRole); 
             // unitOfWork.SaveChangesAsync();
             return entityId;
        }
        
        public async Task<int> UpdateRole(CompanyRolesDto rolePermissions, int companyId, int roleId)
        {
            // var companyId = Convert.ToInt32(userUtility.GetCurrentCompany());
            // var company = unitOfWork.Company.GetCompany(companyId);
            // var companyRoles =await unitOfWork.CompanyRoles.GetCompanyRoles(companyId);
            var compRole = await unitOfWork.CompanyRoles.GetRole(roleId);
            
            // var companyRole = new CompanyRole()
            // {
            if (compRole is null) return 0;
            
            if(compRole.RolePermissions != null && compRole.RolePermissions.Any() )
                unitOfWork.RolePermissions.DeleteRange(compRole.RolePermissions); 
            
            compRole.CompanyId = companyId;
            compRole.Name = rolePermissions.Name;
            // compRole.Company = company;
            compRole.RolePermissions = rolePermissions.RolePermissions.Select(x => new RolePermissions
            {
                PageCode = x.AppPageCode,
                Read = x.CanRead,
                Edit = x.CanEdit,
                Add = x.CanAdd,
                Delete = x.CanDelete,
                Import = x.CanImport,
                Export = x.CanExport,
            }).ToList();

                
                    
            // };
                
            unitOfWork.CompanyRoles.Update(compRole);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CompanyRoleDto>?> GetCompanyRoles(int companyId)
        {
            var companyRoles = await unitOfWork.CompanyRoles.GetCompanyRoles(companyId);
            var result = new List<CompanyRoleDto>();
            foreach (var role in companyRoles)
            {
                
                var compRole = new CompanyRoleDto()
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                result.Add(compRole);
            }
            
            return result;
        }
        
  public List<AppModuleDto> GetRole(int roleId)
{
    // Retrieve company role by ID
    var companyRole = unitOfWork.CompanyRoles.GetRole(roleId).Result;
    if (companyRole is null) return new List<AppModuleDto>();

    // Initialize the result list of AppModuleDto
    var appModules = new List<AppModuleDto>();

    // Group role permissions by their corresponding modules
    var moduleGroups = companyRole.RolePermissions?
        .Where(rp => rp.AppPages is { AppSubModule.AppModule: not null })
        .GroupBy(rp => rp.AppPages.AppSubModule?.AppModuleId)
        .ToList() ?? new List<IGrouping<int?, RolePermissions>>();

    foreach (var moduleGroup in moduleGroups)
    {
        var firstModule = moduleGroup.First().AppPages.AppSubModule?.AppModule;

        var appModuleDto = new AppModuleDto
        {
            Id = firstModule.Id,
            Name = firstModule.Name,
            SubModuleDto = new List<AppSubModuleDto>()
        };

        // Group role permissions by their submodules within the module
        var subModuleGroups = moduleGroup
            .GroupBy(rp => rp.AppPages.AppSubModuleId)
            .ToList();

        foreach (var subModuleGroup in subModuleGroups)
        {
            var firstSubModule = subModuleGroup.First().AppPages.AppSubModule;

            var subModuleDto = new AppSubModuleDto
            {
                Id = firstSubModule.Id,
                Name = firstSubModule.Name,
                PageDto = new List<AppPageDto>()
            };

            foreach (var rolePermission in subModuleGroup)
            {
                var pageDto = new AppPageDto
                {
                    // Id = rolePermission.AppPages.Id,
                    Name = rolePermission.AppPages.Name,
                    ArabicName = rolePermission.AppPages.ArabicName,
                    Code = rolePermission.AppPages.Code,
                    Add = rolePermission.Add,
                    Update = rolePermission.Edit,
                    Delete = rolePermission.Delete,
                    Read = rolePermission.Read,
                    Export = rolePermission.Export,
                    Import = rolePermission.Import,
               
                };
                subModuleDto.PageDto.Add(pageDto);
            }
            
            appModuleDto.SubModuleDto.Add(subModuleDto);
        }

        appModules.Add(appModuleDto);
    }

    return appModules;
}
        
  

        public async Task<int?> UpdateCompanyModules(AppModuleDto appModuleDto, int companyId)
        {
            var companyModule = new CompanyModule()
            {
                CompanyId = companyId,
                AppModuleId = appModuleDto.Id,
            };
            unitOfWork.CompanyModules.Update(companyModule);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<ApiRespone<string>> DeleteRole(int roleId)
        {
            
            var result  = await unitOfWork.CompanyRoles.DeleteRole(roleId); 
            await unitOfWork.SaveChangesAsync();
            return result;
        }
} 
