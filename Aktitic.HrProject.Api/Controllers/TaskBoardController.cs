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
    public async Task<List<TaskBoardReadDto>> GetAll()
    {
        return await _taskBoardManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Task<TaskBoardReadDto?>>> Get(int id)
    {
        var task =await _taskBoardManager.Get(id);
        if (task == null) return NotFound("Task Not Found");
        return Ok(task);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TaskBoardAddDto taskBoardAddDto)
    {
        var task = _taskBoardManager.Add(taskBoardAddDto);
        if (task.Result == 0) return BadRequest("Failed to create");
        return Ok("TaskBoard Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TaskBoardUpdateDto taskBoardUpdateDto,int id)
    {
        var task = _taskBoardManager.Update(taskBoardUpdateDto,id);
        if (task.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var task = _taskBoardManager.Delete(id);
        if (task.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
}