using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskBoardController(ITaskBoardManager taskBoardManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Read))]
    public async Task<List<TaskBoardReadDto>> GetAll()
    {
        return await taskBoardManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Read))]
    public ActionResult<Task<TaskBoardReadDto?>> Get(int id)
    {
        var task = taskBoardManager.Get(id);
        if (task == null) return NotFound("Task Not Found");
        return Ok(task);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Add))]
    public ActionResult<Task> Add( TaskBoardAddDto taskBoardAddDto)
    {
        var result =taskBoardManager.Add(taskBoardAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("TaskBoard Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Edit))]
    public ActionResult Update( TaskBoardUpdateDto taskBoardUpdateDto,int id)
    {
        var result =taskBoardManager.Update(taskBoardUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    { 
        var result =taskBoardManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    //get by project id 
    [HttpGet("getByProjectId/{id}")]
    [AuthorizeRole(nameof(Pages.TaskBoard), nameof(Roles.Read))]
    public async Task<List<TaskBoardReadDto>> GetByProjectId(int id)
    {
        var task = await taskBoardManager.GetAllByProjectId(id);
        
        return task;
    }
    
}