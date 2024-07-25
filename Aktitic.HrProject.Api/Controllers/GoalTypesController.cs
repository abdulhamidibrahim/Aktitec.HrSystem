using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalTypesController: ControllerBase
{
    private readonly IGoalTypeManager _goalTypeManager;

    public GoalTypesController(IGoalTypeManager goalTypeManager)
    {
        _goalTypeManager = goalTypeManager;
    }
    
    [HttpGet]
    public Task<List<GoalTypeReadDto>> GetAll()
    {
        return _goalTypeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<GoalTypeReadDto?> Get(int id)
    {
        var result = _goalTypeManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(GoalTypeAddDto goalTypeAddDto)
    {
        var result = _goalTypeManager.Add(goalTypeAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(GoalTypeUpdateDto goalTypeUpdateDto,int id)
    {
        var result= _goalTypeManager.Update(goalTypeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _goalTypeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredGoalTypes")]
    public Task<FilteredGoalTypeDto> GetFilteredGoalTypesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _goalTypeManager.GetFilteredGoalTypesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<GoalTypeDto>> GlobalSearch(string search,string? column)
    {
        return await _goalTypeManager.GlobalSearch(search,column);
    }


}