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
public class PerformanceAppraisalsController(IPerformanceAppraisalManager performanceAppraisalManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Read))]
    public Task<List<PerformanceAppraisalReadDto>> GetAll()
    {
        return performanceAppraisalManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Read))]
    public ActionResult<PerformanceAppraisalReadDto?> Get(int id)
    {
        var result = performanceAppraisalManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Add))]
    public ActionResult<Task> Add(PerformanceAppraisalAddDto performanceAppraisalAddDto)
    {
        var result = performanceAppraisalManager.Add(performanceAppraisalAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Edit))]
    public ActionResult<Task> Update(PerformanceAppraisalUpdateDto performanceAppraisalUpdateDto,int id)
    {
        var result= performanceAppraisalManager.Update(performanceAppraisalUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= performanceAppraisalManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPerformanceAppraisals")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Read))]
    public Task<FilteredPerformanceAppraisalDto> GetFilteredPerformanceAppraisalsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return performanceAppraisalManager.GetFilteredPerformanceAppraisalsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.PerformanceAppraisal), nameof(Roles.Read))]
    public async Task<IEnumerable<PerformanceAppraisalDto>> GlobalSearch(string search,string? column)
    {
        return await performanceAppraisalManager.GlobalSearch(search,column);
    }


}