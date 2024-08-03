using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicensesController(ILicenseManager overtimeManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LicenseReadDto>>> GetAll()
    {
        return await overtimeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<LicenseReadDto?> Get(int id)
    {
        var overtime = overtimeManager.Get(id);
        if (overtime == null) return NotFound("License not found");
        return Ok(overtime);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] LicenseAddDto overtimeAddDto)
    {
        var result =overtimeManager.Add(overtimeAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("License added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] LicenseUpdateDto overtimeUpdateDto,int id)
    {
        var result =overtimeManager.Update(overtimeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("License updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =overtimeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<LicenseDto>> GlobalSearch(string search,string? column)
    {
        return await overtimeManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredLicenses")]
    public Task<FilteredLicensesDto> GetFilteredLicensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return overtimeManager.GetFilteredLicenseAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}