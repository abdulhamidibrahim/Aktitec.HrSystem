using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController(ICandidatesManager candidatesManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CandidatesReadDto>>> GetAll()
    {
        var candidates = await candidatesManager.GetAll();
        return Ok(candidates);
    }
    
    [HttpGet("{id}")]
    public ActionResult<CandidatesReadDto?> Get(int id)
    {
        var candidates = candidatesManager.Get(id);
        if (candidates == null) return NotFound("Candidates not found");
        return Ok(candidates);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] CandidatesAddDto candidatesAddDto)
    {
        var result =candidatesManager.Add(candidatesAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Candidates added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] CandidatesUpdateDto candidatesUpdateDto,int id)
    {
        var result =candidatesManager.Update(candidatesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Candidates updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =candidatesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<CandidatesDto>> GlobalSearch(string search,string? column)
    {
        return await candidatesManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredCandidates")]
    public Task<FilteredCandidatesDto> GetFilteredCandidatesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return candidatesManager.GetFilteredCandidatesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}