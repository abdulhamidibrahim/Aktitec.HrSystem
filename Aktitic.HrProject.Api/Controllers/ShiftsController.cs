using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController: ControllerBase
{
    private readonly IShiftManager _shiftManager;

    public ShiftsController(IShiftManager shiftManager)
    {
        _shiftManager = shiftManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ShiftReadDto>>> GetAll()
    {
        var result =  await _shiftManager.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ShiftReadDto?> Get(int id)
    {
        var user = _shiftManager.Get(id);
        if (user == null) return NotFound("Shift not found");
        return Ok(user);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Add([FromBody] ShiftAddDto shiftAddDto)
    {
        var result = await _shiftManager.Add(shiftAddDto);
        if(result== null) return BadRequest("Shift not added");    
        return Ok("Shift added");
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult> Update([FromBody] ShiftUpdateDto shiftUpdateDto,int id)
    {
        var result =await _shiftManager.Update(shiftUpdateDto,id);
        if(result == null) return BadRequest("Shift not updated");
        return Ok("Shift updated");
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result =await _shiftManager.Delete(id);
        if(result == null) return BadRequest("Shift not deleted");
        return Ok("Shift deleted");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ShiftDto>> GlobalSearch(string search,string? column)
    {
        return await _shiftManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredShifts")]
    public Task<FilteredShiftsDto> GetFilteredShiftsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _shiftManager.GetFilteredShiftsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}