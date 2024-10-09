using Aktitic.HrProject.BL.Dtos.AppModules;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AppModulesController(
    IAppModulesManager appModulesManager
    // ,AuthorizationHelperService authorizationHelperService
    ) : ControllerBase
{
    private static string PageCode { get; } = "AppModules";
    
    // [Authorize("PermissionPolicy")]
    [HttpGet]
    public async Task<ActionResult<List<AppModuleDto>>> GetAll()
    {
        // authorizationHelperService.CheckPermissionAsync("Read", PageCode);
        
        var result = await appModulesManager.GetAll();
        return Ok(result);
    }
    
    // [HttpGet("{id}")]
    // public ActionResult<AppModuleDto> Get(int id)
    // {
    //     var result = appModulesManager.Get(id);
    //     return Ok(result);
    // }
    
    [HttpPost("AssignComapnyModules/{companyId}")]
    public async Task<ActionResult<CompanyModuleAddDto>> AssignComapnyModules
        ([FromBody] List<AppModuleDto> appModuleDto, int companyId)
    {
        var result = await appModulesManager.AssignComapnyModules(appModuleDto,companyId);
        if(result is null || result == 0) return BadRequest("Failed to assign modules to company");
        return Ok(result);
    }   

    [HttpGet("GetCompanyModules/{companyId}")]
    public async Task<ActionResult<List<AppModuleDto>>> GetCompanyModules(int companyId)    
    {
        var result = await appModulesManager.GetCompanyModules(companyId);
        return Ok(result);
    }
    
    [HttpPut("UpdateCompanyModules/{companyId}")]
    public async Task<ActionResult<AppModuleDto>> UpdateCompanyModules
        ([FromBody] AppModuleDto appModuleDto, int companyId)
    {
        var result = await appModulesManager.UpdateCompanyModules(appModuleDto,companyId);
        return Ok(result);
    }
    
    [HttpDelete("DeleteRole/{roleId}")]
    public async Task<ActionResult<AppModuleDto>> DeleteRole(int roleId)
    {
        var result = await appModulesManager.DeleteRole(roleId);
        if(!result.Success) return BadRequest(result.Data);
        return Ok(result.Data);
    }
    
    [HttpPost("CreateRole/{companyId}")]
    public async Task<ActionResult<CompanyModuleAddDto>> AssignRoles
        ([FromBody] CompanyRolesDto companyRoles, int companyId)
    {
        var result = await appModulesManager.CreateRole(companyRoles,companyId);
        if(result == 0) return BadRequest("Failed to assign roles to company");
        return Ok(new {Id = result});
    }
    
    [HttpGet("GetCompanyRoles/{companyId}")]
    public async Task<ActionResult<List<CompanyRolesDto>>> GetCompanyRoles(int companyId)
    {
        var result = await appModulesManager.GetCompanyRoles(companyId);
        return Ok(result);
    }
    
    [HttpGet("GetRole/{roleId}")]
    public ActionResult<List<CompanyRolesDto>> GetRole(int roleId)
    {
        var result = appModulesManager.GetRole(roleId);
        return Ok(result);
    }
    
    [HttpPut("UpdateRole/{roleId}")]
    public async Task<ActionResult<List<CompanyRolesDto>>> UpdateCompanyRoles(CompanyRolesDto roles,int companyId,int roleId)
    {
        var result = await appModulesManager.UpdateRole(roles,companyId,roleId);
        if(result == 0) return BadRequest("Failed to update role");
        return Ok("updated successfully !");
    }
}
