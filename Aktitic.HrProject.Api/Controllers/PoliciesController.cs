using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController(IPoliciesManager policyManager) : ControllerBase
{
    [HttpGet, AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Read))]
    public Task<List<PoliciesReadDto>> GetAll()
    {
        return policyManager.GetAll();
    }

    [HttpGet("{id}"), AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Read))]
    public ActionResult<PoliciesReadDto?> Get(int id)
    {
        var result = policyManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [Consumes("multipart/form-data")]
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Add))]
    public ActionResult<Task> Add([FromForm] PoliciesAddDto policyAddDto)
    {
        var result = policyManager.Add(policyAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [Consumes("multipart/form-data")]
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Edit))]
    public ActionResult<Task> Update([FromForm] PoliciesUpdateDto policyUpdateDto,int id)
    {
        var result= policyManager.Update(policyUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= policyManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPolicies")]
    [AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Read))]
    public Task<FilteredPoliciesDto> GetFilteredPoliciesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return policyManager.GetFilteredPoliciesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Policies), nameof(Roles.Read))]
    public async Task<IEnumerable<PolicyDto>> GlobalSearch(string search,string? column)
    {
        return await policyManager.GlobalSearch(search,column);
    }


}