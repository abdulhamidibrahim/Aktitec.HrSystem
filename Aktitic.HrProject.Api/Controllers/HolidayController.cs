using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HolidayController(IHolidayManager holidayManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Read))]
    public ActionResult<List<HolidayReadDto>> GetAll()
    {
        return holidayManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Read))]
    public ActionResult<HolidayReadDto?> Get(int id)
    {
        var result = holidayManager.Get(id);
        if (result == null) return NotFound();
        return result;
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Add))]
    public ActionResult Add([FromBody] HolidayAddDto holidayAddDto)
    {
        var result =holidayManager.Add(holidayAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Holiday added!");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Edit))]
    public ActionResult Update([FromBody] HolidayUpdateDto holidayUpdateDto,int id)
    { 
        var result=holidayManager.Update(holidayUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Holiday updated!");
    }
    
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    {
        var result= holidayManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Holiday deleted!");
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Read))]
    public async Task<IEnumerable<HolidayDto>> GlobalSearch(string search,string? column)
    {
        return await holidayManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredHolidays")]
    [AuthorizeRole(nameof(Pages.Holidays),nameof(Roles.Read))]
    public Task<FilteredHolidayDto> GetFilteredHolidaysAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return holidayManager.GetFilteredHolidaysAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}