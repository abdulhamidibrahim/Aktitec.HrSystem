using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperiencesController(IExperienceManager experiencesManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ExperienceReadDto>>> GetAll()
    {
        var experiences = await experiencesManager.GetAll();
        return Ok(experiences);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ExperienceReadDto?> Get(int id)
    {
        var experiences = experiencesManager.Get(id);
        if (experiences == null) return NotFound("Experiences not found");
        return Ok(experiences);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] ExperienceAddDto experiencesAddDto)
    {
        var result =experiencesManager.Add(experiencesAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Experiences added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] ExperienceUpdateDto experiencesUpdateDto,int id)
    {
        var result =experiencesManager.Update(experiencesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Experiences updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =experiencesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ExperienceDto>> GlobalSearch(string search,string? column)
    {
        return await experiencesManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredExperiences")]
    public Task<FilteredExperiencesDto> GetFilteredExperiencesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return experiencesManager.GetFilteredExperienceAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}