using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceAppraisalsController: ControllerBase
{
    private readonly IPerformanceAppraisalManager _performanceAppraisalManager;

    public PerformanceAppraisalsController(IPerformanceAppraisalManager performanceAppraisalManager)
    {
        _performanceAppraisalManager = performanceAppraisalManager;
    }
    
    [HttpGet]
    public Task<List<PerformanceAppraisalReadDto>> GetAll()
    {
        return _performanceAppraisalManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PerformanceAppraisalReadDto?> Get(int id)
    {
        var result = _performanceAppraisalManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PerformanceAppraisalAddDto performanceAppraisalAddDto)
    {
        var result = _performanceAppraisalManager.Add(performanceAppraisalAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PerformanceAppraisalUpdateDto performanceAppraisalUpdateDto,int id)
    {
        var result= _performanceAppraisalManager.Update(performanceAppraisalUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _performanceAppraisalManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPerformanceAppraisals")]
    public Task<FilteredPerformanceAppraisalDto> GetFilteredPerformanceAppraisalsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _performanceAppraisalManager.GetFilteredPerformanceAppraisalsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PerformanceAppraisalDto>> GlobalSearch(string search,string? column)
    {
        return await _performanceAppraisalManager.GlobalSearch(search,column);
    }


}