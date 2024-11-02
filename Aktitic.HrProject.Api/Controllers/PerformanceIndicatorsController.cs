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
public class PerformanceIndicatorsController(IPerformanceIndicatorManager performanceIndicatorManager) : ControllerBase
{
    [HttpGet]
   [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Read))]
    public Task<List<PerformanceIndicatorReadDto>> GetAll()
    {
        return performanceIndicatorManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Read))]
    public ActionResult<PerformanceIndicatorReadDto?> Get(int id)
    {
        var result = performanceIndicatorManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Add))]
    public ActionResult<Task> Add(PerformanceIndicatorAddDto performanceIndicatorAddDto)
    {
        var result = performanceIndicatorManager.Add(performanceIndicatorAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Edit))]
    public ActionResult<Task> Update(PerformanceIndicatorUpdateDto performanceIndicatorUpdateDto,int id)
    {
        var result= performanceIndicatorManager.Update(performanceIndicatorUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= performanceIndicatorManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPerformanceIndicators")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Read))]
    public Task<FilteredPerformanceIndicatorDto> GetFilteredPerformanceIndicatorsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return performanceIndicatorManager.GetFilteredPerformanceIndicatorsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.PerformanceIndicator), nameof(Roles.Read))]
    public async Task<IEnumerable<PerformanceIndicatorDto>> GlobalSearch(string search,string? column)
    {
        return await performanceIndicatorManager.GlobalSearch(search,column);
    }


}