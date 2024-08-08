using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController(IJobsManager jobsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<JobsReadDto>>> GetAll()
    {
        var jobs = await jobsManager.GetAll();
        return Ok(jobs);
    }
    
    [HttpGet("{id}")]
    public ActionResult<JobsReadDto?> Get(int id)
    {
        var jobs = jobsManager.Get(id);
        if (jobs == null) return NotFound("Jobs not found");
        return Ok(jobs);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] JobsAddDto jobsAddDto)
    {
        var result =jobsManager.Add(jobsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Jobs added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] JobsUpdateDto jobsUpdateDto,int id)
    {
        var result =jobsManager.Update(jobsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Jobs updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =jobsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<JobsDto>> GlobalSearch(string search,string? column)
    {
        return await jobsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredJobs")]
    public Task<FilteredJobsDto> GetFilteredJobsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return jobsManager.GetFilteredJobsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}