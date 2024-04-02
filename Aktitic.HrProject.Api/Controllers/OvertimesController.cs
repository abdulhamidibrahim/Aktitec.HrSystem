using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OvertimesController: ControllerBase
{
    private readonly IOvertimeManager _overtimeManager;

    public OvertimesController(IOvertimeManager overtimeManager)
    {
        _overtimeManager = overtimeManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<OvertimeReadDto>>> GetAll()
    {
        return await _overtimeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OvertimeReadDto?>> Get(int id)
    {
        var overtime =await _overtimeManager.Get(id);
        if (overtime == null) return NotFound("Overtime not found");
        return Ok(overtime);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] OvertimeAddDto overtimeAddDto)
    {
        var result =_overtimeManager.Add(overtimeAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Overtime added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] OvertimeUpdateDto overtimeUpdateDto,int id)
    {
        var result =_overtimeManager.Update(overtimeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Overtime updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =_overtimeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<OvertimeDto>> GlobalSearch(string search,string? column)
    {
        return await _overtimeManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredOvertimes")]
    public Task<FilteredOvertimeDto> GetFilteredOvertimesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _overtimeManager.GetFilteredOvertimesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}