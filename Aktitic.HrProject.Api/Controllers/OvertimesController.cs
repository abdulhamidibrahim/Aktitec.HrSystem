using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OvertimesController(IOvertimeManager overtimeManager) : ControllerBase
{
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Read))]
    public async Task<ActionResult<List<OvertimeReadDto>>> GetAll()
    {
        return await overtimeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Read))]
    public async Task<ActionResult<OvertimeReadDto?>> Get(int id)
    {
        var overtime =await overtimeManager.Get(id);
        if (overtime == null) return NotFound("Overtime not found");
        return Ok(overtime);
    }
    
    [HttpPost("create")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Add))]
    public ActionResult Add([FromBody] OvertimeAddDto overtimeAddDto)
    {
        var result =overtimeManager.Add(overtimeAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Overtime added successfully");
    }
    
    [HttpPut("update/{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Edit))]
    public ActionResult Update([FromBody] OvertimeUpdateDto overtimeUpdateDto,int id)
    {
        var result =overtimeManager.Update(overtimeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Overtime updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    { 
        var result =overtimeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Read))]
    public async Task<IEnumerable<OvertimeDto>> GlobalSearch(string search,string? column)
    {
        return await overtimeManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredOvertimes")]
    [HttpGet, AuthorizeRole(nameof(Pages.Overtime), nameof(Roles.Read))]
    public Task<FilteredOvertimeDto> GetFilteredOvertimesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return overtimeManager.GetFilteredOvertimesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}