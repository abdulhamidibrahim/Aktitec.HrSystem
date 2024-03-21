using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTask.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController: ControllerBase
{
    private readonly ITaskManager _taskManager;

    public TasksController(ITaskManager taskManager)
    {
        _taskManager = taskManager;
    }
    
    [HttpGet]
    public Task<List<TaskReadDto>> GetAll()
    {
        return _taskManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Task<TaskReadDto?>> Get(int id)
    {
        var user = _taskManager.Get(id);
        if (user == null) return NotFound("Task not found!");
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TaskAddDto taskAddDto)
    {
        var result =_taskManager.Add(taskAddDto);
        if(result.Result ==0 ) return BadRequest("Failed to add task!");
        return Ok("Task added successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TaskUpdateDto taskUpdateDto,int id)
    {
        var result = _taskManager.Update(taskUpdateDto,id);
        if(result.Result ==0 ) return BadRequest("Failed to update task!");
        return Ok("Task updated successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var task = _taskManager.Delete(id);
        if (task.Result == 0) return NotFound("Task not found!");
        return Ok("Task deleted successfully!");
    }
    
    [HttpGet("getFilteredTasks")]
    public Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _taskManager.GetFilteredTasksAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}