using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskBoardController: ControllerBase
{
    private readonly ITaskBoardManager _taskBoardManager;

    public TaskBoardController(ITaskBoardManager taskBoardManager)
    {
        _taskBoardManager = taskBoardManager;
    }
    
    [HttpGet]
    public Task<List<TaskBoardReadDto>> GetAll()
    {
        return _taskBoardManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Task<TaskBoardReadDto?>> Get(int id)
    {
        var user = _taskBoardManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TaskBoardAddDto taskBoardAddDto)
    {
        _taskBoardManager.Add(taskBoardAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TaskBoardUpdateDto taskBoardUpdateDto,int id)
    {
        _taskBoardManager.Update(taskBoardUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        _taskBoardManager.Delete(id);
        return Ok();
    }
    
}