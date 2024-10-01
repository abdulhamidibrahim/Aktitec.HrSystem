using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicantsController(IJobApplicantsManager jobApplicantsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<JobApplicantsReadDto>>> GetAll()
    {
        var jobApplicants = await jobApplicantsManager.GetAll();
        return Ok(jobApplicants);
    }
    
    [HttpGet("{id}")]
    public ActionResult<JobApplicantsReadDto?> Get(int id)
    {
        var jobApplicants = jobApplicantsManager.Get(id);
        if (jobApplicants == null) return NotFound("JobApplicants not found");
        return Ok(jobApplicants);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] JobApplicantsAddDto jobApplicantsAddDto)
    {
        try
        {
            var result =jobApplicantsManager.Add(jobApplicantsAddDto);
            if (result.Result == 0) return BadRequest("Failed to add");    
            return Ok("JobApplicants added successfully");
        }
        catch (Exception e)
        {
            // return BadRequest(e);
            Console.WriteLine(e);
            throw;
        }

    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] JobApplicantsUpdateDto jobApplicantsUpdateDto,int id)
    {
        var result =jobApplicantsManager.Update(jobApplicantsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("JobApplicants updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =jobApplicantsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<JobApplicantsDto>> GlobalSearch(string search,string? column)
    {
        return await jobApplicantsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredJobApplicants")]
    public Task<FilteredJobApplicantsDto> GetFilteredJobApplicantsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return jobApplicantsManager.GetFilteredJobApplicantsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GetTotalCount")]
    public async Task<object> GetTotalCount()
    {
        return await jobApplicantsManager.GetTotalCount();
    }
    
}