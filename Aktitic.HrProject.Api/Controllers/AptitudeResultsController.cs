using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AptitudeResultsController(IAptitudeResultsManager aptitudeResultsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AptitudeResultsReadDto>>> GetAll()
    {
        var aptitudeResults = await aptitudeResultsManager.GetAll();
        return Ok(aptitudeResults);
    }
    
    [HttpGet("{id}")]
    public ActionResult<AptitudeResultsReadDto?> Get(int id)
    {
        var aptitudeResults = aptitudeResultsManager.Get(id);
        if (aptitudeResults == null) return NotFound("AptitudeResults not found");
        return Ok(aptitudeResults);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] AptitudeResultsAddDto aptitudeResultsAddDto)
    {
        var result =aptitudeResultsManager.Add(aptitudeResultsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("AptitudeResults added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] AptitudeResultsUpdateDto aptitudeResultsUpdateDto,int id)
    {
        var result =aptitudeResultsManager.Update(aptitudeResultsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("AptitudeResults updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =aptitudeResultsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<AptitudeResultsDto>> GlobalSearch(string search,string? column)
    {
        return await aptitudeResultsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredAptitudeResults")]
    public Task<FilteredAptitudeResultsDto> GetFilteredAptitudeResultsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return aptitudeResultsManager.GetFilteredAptitudeResultsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}