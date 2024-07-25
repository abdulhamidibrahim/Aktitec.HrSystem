using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceIndicatorsController: ControllerBase
{
    private readonly IPerformanceIndicatorManager _performanceIndicatorManager;

    public PerformanceIndicatorsController(IPerformanceIndicatorManager performanceIndicatorManager)
    {
        _performanceIndicatorManager = performanceIndicatorManager;
    }
    
    [HttpGet]
    public Task<List<PerformanceIndicatorReadDto>> GetAll()
    {
        return _performanceIndicatorManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PerformanceIndicatorReadDto?> Get(int id)
    {
        var result = _performanceIndicatorManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PerformanceIndicatorAddDto performanceIndicatorAddDto)
    {
        var result = _performanceIndicatorManager.Add(performanceIndicatorAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PerformanceIndicatorUpdateDto performanceIndicatorUpdateDto,int id)
    {
        var result= _performanceIndicatorManager.Update(performanceIndicatorUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _performanceIndicatorManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPerformanceIndicators")]
    public Task<FilteredPerformanceIndicatorDto> GetFilteredPerformanceIndicatorsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _performanceIndicatorManager.GetFilteredPerformanceIndicatorsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PerformanceIndicatorDto>> GlobalSearch(string search,string? column)
    {
        return await _performanceIndicatorManager.GlobalSearch(search,column);
    }


}