using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesignationsController: ControllerBase
{
    private readonly IDesignationManager _designationManager;

    public DesignationsController(IDesignationManager designationManager)
    {
        _designationManager = designationManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<DesignationReadDto>>> GetAll()
    {
        var result = await _designationManager.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DesignationReadDto?>> Get(int id)
    {
        var designation = await _designationManager.Get(id);

        if (designation == null)
        {
            return NotFound("Designation Not Found!");
        }

        return Ok(designation);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] DesignationAddDto designationAddDto)
    {
        var result = _designationManager.Add(designationAddDto);
        if (result.Result == 0) return BadRequest("Designation Already Exists!");
        return Ok("Designation Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] DesignationUpdateDto designationUpdateDto,int id)
    {
        var result = _designationManager.Update(designationUpdateDto,id);
        if (result.Result == 0) return BadRequest("Designation Not Found!");
        return Ok("Designation Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _designationManager.Delete(id);
        if (result.Result == 0) return BadRequest("Designation Not Found!");
        return Ok("Designation Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<DesignationDto>> GlobalSearch(string search,string? column)
    {
        return await _designationManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredDesignations")]
    public Task<FilteredDesignationDto> GetFilteredDesignationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _designationManager.GetFilteredDesignationsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }

}