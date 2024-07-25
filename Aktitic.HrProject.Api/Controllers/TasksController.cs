using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
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
        var result = _taskManager.Get(id);
        if (result == null) return NotFound("Task not found!");
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TaskAddDto taskAddDto)
    {
        var result =_taskManager.Add(taskAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");      
        return Ok("Task added successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update(TaskUpdateDto taskUpdateDto,int id)
    {
        var result = _taskManager.Update(taskUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Task updated successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result =_taskManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Task deleted successfully!");
    }
    
    // [HttpGet("getFilteredTasks")]
    // public Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    // {
    //     
    //     return _taskManager.GetFilteredTasksAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    // }
    //
    // [HttpGet("globalSearch")]
    // public Task<List<TaskDto>> GlobalSearch(string searchKey,string? column)
    // {
    //     return _taskManager.GlobalSearch(searchKey,column);
    // }
    
    [HttpGet("getTaskWithProjectId/{projectId}")]
    public Task<List<TaskDto>> GetTaskWithProjectId(int projectId)
    {
        return _taskManager.GetTaskWithProjectId(projectId);
    }
    
    [HttpGet("getTaskByCompleted/{completed}")]
    public Task<List<TaskDto>> GetTaskByCompleted(bool completed)
    {
        return _taskManager.GetTaskByCompleted(completed);
    }
}