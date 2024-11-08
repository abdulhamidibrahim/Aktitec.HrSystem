using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController(ITimesheetManager timesheetManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TimesheetReadDto>>> GetAll()
    {
        return await timesheetManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TimesheetReadDto?>> Get(int id)
    {
        var result = await timesheetManager.Get(id);
        if (result == null) return NotFound("TimeSheet not found");
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] TimesheetAddDto timesheetAddDto)
    {
        var result = timesheetManager.Add(timesheetAddDto);
        if (result.Result == Task.FromResult(0)) return BadRequest("Failed to create");
        return Ok("Created successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] TimesheetUpdateDto timesheetUpdateDto,int id)
    {
        var result =timesheetManager.Update(timesheetUpdateDto,id);
        if (result.Result == Task.FromResult(0)) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =timesheetManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    [HttpGet("getFilteredTimeSheets")]
    public Task<FilteredTimeSheetDto> GetFilteredTimeSheetsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return timesheetManager.GetFilteredTimeSheetsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TimeSheetDto>> GlobalSearch(string search,string? column)
    {
        return await timesheetManager.GlobalSearch(search,column);
    }

}