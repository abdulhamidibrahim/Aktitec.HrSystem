using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalTypesController(IGoalTypeManager goalTypeManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Read))]
    public Task<List<GoalTypeReadDto>> GetAll()
    {
        return goalTypeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Read))]
    public ActionResult<GoalTypeReadDto?> Get(int id)
    {
        var result = goalTypeManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Add))]
    public ActionResult<Task> Add(GoalTypeAddDto goalTypeAddDto)
    {
        var result = goalTypeManager.Add(goalTypeAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Edit))]
    public ActionResult<Task> Update(GoalTypeUpdateDto goalTypeUpdateDto,int id)
    {
        var result= goalTypeManager.Update(goalTypeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= goalTypeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredGoalTypes")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Read))]
    public Task<FilteredGoalTypeDto> GetFilteredGoalTypesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return goalTypeManager.GetFilteredGoalTypesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.GoalType),nameof(Roles.Read))]
    public async Task<IEnumerable<GoalTypeDto>> GlobalSearch(string search,string? column)
    {
        return await goalTypeManager.GlobalSearch(search,column);
    }


}