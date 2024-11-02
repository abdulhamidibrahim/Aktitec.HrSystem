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
public class GoalListsController(IGoalListManager goalListManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Read))]
    public Task<List<GoalListReadDto>> GetAll()
    {
        return goalListManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Read))]
    public ActionResult<GoalListReadDto?> Get(int id)
    {
        var result = goalListManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Add))]
    public ActionResult<Task> Add(GoalListAddDto goalListAddDto)
    {
        var result = goalListManager.Add(goalListAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Edit))]
    public ActionResult<Task> Update(GoalListUpdateDto goalListUpdateDto,int id)
    {
        var result= goalListManager.Update(goalListUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= goalListManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredGoalLists")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Read))]
    public Task<FilteredGoalListDto> GetFilteredGoalListsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return goalListManager.GetFilteredGoalListsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.GoalList),nameof(Roles.Read))]
    public async Task<IEnumerable<GoalListDto>> GlobalSearch(string search,string? column)
    {
        return await goalListManager.GlobalSearch(search,column);
    }


}