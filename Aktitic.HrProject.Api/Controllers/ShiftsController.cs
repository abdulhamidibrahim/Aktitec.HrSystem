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
    public ActionResult Add([FromBody] ShiftAddDto shiftAddDto)
    {
        var result =_shiftManager.Add(shiftAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Shift added");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] ShiftUpdateDto shiftUpdateDto,int id)
    {
        var result =_shiftManager.Update(shiftUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Shift updated");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =_shiftManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
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