using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalListsController: ControllerBase
{
    private readonly IGoalListManager _goalListManager;

    public GoalListsController(IGoalListManager goalListManager)
    {
        _goalListManager = goalListManager;
    }
    
    [HttpGet]
    public Task<List<GoalListReadDto>> GetAll()
    {
        return _goalListManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<GoalListReadDto?> Get(int id)
    {
        var result = _goalListManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(GoalListAddDto goalListAddDto)
    {
        var result = _goalListManager.Add(goalListAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(GoalListUpdateDto goalListUpdateDto,int id)
    {
        var result= _goalListManager.Update(goalListUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _goalListManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredGoalLists")]
    public Task<FilteredGoalListDto> GetFilteredGoalListsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _goalListManager.GetFilteredGoalListsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<GoalListDto>> GlobalSearch(string search,string? column)
    {
        return await _goalListManager.GlobalSearch(search,column);
    }


}