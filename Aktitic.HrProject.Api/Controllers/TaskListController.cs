using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrTaskList.BL;
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
        var user = _taskManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TaskListAddDto taskAddDto)
    {
        _taskManager.Add(taskAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TaskListUpdateDto taskUpdateDto,int id)
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
    
}