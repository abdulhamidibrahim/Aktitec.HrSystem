using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController: ControllerBase
{
    private readonly IPoliciesManager _policyManager;

    public PoliciesController(IPoliciesManager policyManager)
    {
        _policyManager = policyManager;
    }
    
    [HttpGet]
    public Task<List<PoliciesReadDto>> GetAll()
    {
        return _policyManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PoliciesReadDto?> Get(int id)
    {
        var result = _policyManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [Consumes("multipart/form-data")]
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] PoliciesAddDto policyAddDto)
    {
        var result = _policyManager.Add(policyAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [Consumes("multipart/form-data")]
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update([FromForm] PoliciesUpdateDto policyUpdateDto,int id)
    {
        var result= _policyManager.Update(policyUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _policyManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPolicies")]
    public Task<FilteredPoliciesDto> GetFilteredPoliciesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _policyManager.GetFilteredPoliciesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PolicyDto>> GlobalSearch(string search,string? column)
    {
        return await _policyManager.GlobalSearch(search,column);
    }


}