using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HolidayController: ControllerBase
{
    private readonly IHolidayManager _holidayManager;

    public HolidayController(IHolidayManager holidayManager)
    {
        _holidayManager = holidayManager;
    }
    
    [HttpGet]
    public ActionResult<List<HolidayReadDto>> GetAll()
    {
        return _holidayManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<HolidayReadDto?> Get(int id)
    {
        var user = _holidayManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] HolidayAddDto holidayAddDto)
    {
        var result =_holidayManager.Add(holidayAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Holiday added!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] HolidayUpdateDto holidayUpdateDto,int id)
    { 
        var result=_holidayManager.Update(holidayUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Holiday updated!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result= _holidayManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Holiday deleted!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<HolidayDto>> GlobalSearch(string search,string? column)
    {
        return await _holidayManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredHolidays")]
    public Task<FilteredHolidayDto> GetFilteredHolidaysAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _holidayManager.GetFilteredHolidaysAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}