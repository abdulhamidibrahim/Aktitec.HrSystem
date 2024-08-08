using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicensesController(ILicenseManager licensesManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LicenseReadDto>>> GetAll()
    {
        return await licensesManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<LicenseReadDto?> Get(int id)
    {
        var licenses = licensesManager.Get(id);
        if (licenses == null) return NotFound("License not found");
        return Ok(licenses);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] LicenseAddDto licensesAddDto)
    {
        var result =licensesManager.Add(licensesAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("License added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] LicenseUpdateDto licensesUpdateDto,int id)
    {
        var result =licensesManager.Update(licensesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("License updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =licensesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<LicenseDto>> GlobalSearch(string search,string? column)
    {
        return await licensesManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredLicenses")]
    public Task<FilteredLicensesDto> GetFilteredLicensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return licensesManager.GetFilteredLicenseAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}