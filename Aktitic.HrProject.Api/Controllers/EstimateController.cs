using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstimateController(IEstimateManager estimateManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Read))]
    public Task<List<EstimateReadDto>> GetAll()
    {
        return estimateManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Read))]
    public ActionResult<EstimateReadDto?> Get(int id)
    {
        var result = estimateManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Add))]
    public ActionResult<Task> Add(EstimateAddDto estimateAddDto)
    {
        var result = estimateManager.Add(estimateAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Edit))]
    public ActionResult<Task> Update(EstimateUpdateDto estimateUpdateDto,int id)
    {
        var result= estimateManager.Update(estimateUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= estimateManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredEstimates")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Read))]
    public Task<FilteredEstimateDto> GetFilteredEstimatesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return estimateManager.GetFilteredEstimatesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Read))]
    public async Task<IEnumerable<EstimateDto>> GlobalSearch(string search,string? column)
    {
        return await estimateManager.GlobalSearch(search,column);
    }


}