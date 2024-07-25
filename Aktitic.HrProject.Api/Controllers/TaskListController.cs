using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskListController: ControllerBase
{
    private readonly ITaskListManager _taskManager;

    public TaskListController(ITaskListManager taskManager)
    {
        _taskManager = taskManager;
    }
    
    [HttpGet]
    public Task<List<TaskListReadDto>> GetAll()
    {
        return _taskManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Task<TaskListReadDto?>> Get(int id)
    {
        var result = _taskManager.Get(id);
        if (result == null) return NotFound("TaskList not found");
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add( TaskListAddDto taskAddDto)
    {
        var result=_taskManager.Add(taskAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("TaskList Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update(TaskListUpdateDto taskUpdateDto,int id)
    {
        var result =_taskManager.Update(taskUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result =_taskManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    // get by taskBoardId
    [HttpGet("getByTaskBoardId/{id}")]
    public Task<List<TaskListReadDto>> GetByTaskBoardId(int id)
    {
        return _taskManager.GetAllByTaskBoardId(id);
    }
}