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
    public ActionResult<Task<TaskBoardReadDto?>> Get(int id)
    {
        var task = _taskBoardManager.Get(id);
        if (task == null) return NotFound("Task Not Found");
        return Ok(task);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add( TaskBoardAddDto taskBoardAddDto)
    {
        var result =_taskBoardManager.Add(taskBoardAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("TaskBoard Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update( TaskBoardUpdateDto taskBoardUpdateDto,int id)
    {
        var result =_taskBoardManager.Update(taskBoardUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    { 
        var result =_taskBoardManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    //get by project id 
    [HttpGet("getByProjectId/{id}")]
    public async Task<List<TaskBoardReadDto>> GetByProjectId(int id)
    {
        var task = await _taskBoardManager.GetAllByProjectId(id);
        
        return task;
    }
    
}