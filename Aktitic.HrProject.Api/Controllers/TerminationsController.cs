using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TerminationsController: ControllerBase
{
    private readonly ITerminationManager _terminationsManager;

    public TerminationsController(ITerminationManager terminationManager)
    {
        _terminationsManager = terminationManager;
    }
    
    [HttpGet]
    public Task<List<TerminationReadDto>> GetAll()
    {
        return _terminationsManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TerminationReadDto?> Get(int id)
    {
        var result = _terminationsManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add( TerminationAddDto terminationsAddDto)
    {
        var result = _terminationsManager.Add(terminationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update( TerminationUpdateDto terminationsUpdateDto,int id)
    {
        var result= _terminationsManager.Update(terminationsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _terminationsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTerminations")]
    public Task<FilteredTerminationsDto> GetFilteredTerminationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _terminationsManager.GetFilteredTerminationAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TerminationDto>> GlobalSearch(string search,string? column)
    {
        return await _terminationsManager.GlobalSearch(search,column);
    }


}