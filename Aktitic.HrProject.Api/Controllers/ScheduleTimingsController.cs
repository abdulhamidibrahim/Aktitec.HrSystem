using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleTimingsController(IScheduleTimingsManager scheduleTimingsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ScheduleTimingsReadDto>>> GetAll()
    {
        var scheduleTimings = await scheduleTimingsManager.GetAll();
        return Ok(scheduleTimings);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ScheduleTimingsReadDto?> Get(int id)
    {
        var scheduleTimings = scheduleTimingsManager.Get(id);
        if (scheduleTimings == null) return NotFound("ScheduleTimings not found");
        return Ok(scheduleTimings);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] ScheduleTimingsAddDto scheduleTimingsAddDto)
    {
        var result =scheduleTimingsManager.Add(scheduleTimingsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("ScheduleTimings added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] ScheduleTimingsUpdateDto scheduleTimingsUpdateDto,int id)
    {
        var result =scheduleTimingsManager.Update(scheduleTimingsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("ScheduleTimings updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =scheduleTimingsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ScheduleTimingsDto>> GlobalSearch(string search,string? column)
    {
        return await scheduleTimingsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredScheduleTimings")]
    public Task<FilteredScheduleTimingsDto> GetFilteredScheduleTimingsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return scheduleTimingsManager.GetFilteredScheduleTimingsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}