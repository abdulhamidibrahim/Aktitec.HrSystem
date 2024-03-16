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
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TaskAddDto taskAddDto)
    {
        _taskManager.Add(taskAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TaskUpdateDto taskUpdateDto,int id)
    {
        _taskManager.Update(taskUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        _taskManager.Delete(id);
        return Ok();
    }
    
    [HttpGet("getFilteredTasks")]
    public Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _taskManager.GetFilteredTasksAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}