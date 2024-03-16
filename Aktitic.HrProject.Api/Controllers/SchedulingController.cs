using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulingController: ControllerBase
{
    private readonly ISchedulingManager _schedulingManager;

    public SchedulingController(ISchedulingManager schedulingManager)
    {
        _schedulingManager = schedulingManager;
    }
    
    [HttpGet]
    public ActionResult<List<SchedulingReadDto>> GetAll()
    {
        return _schedulingManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<SchedulingReadDto?> Get(int id)
    {
        var user = _schedulingManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] SchedulingAddDto schedulingAddDto)
    {
        _schedulingManager.Add(schedulingAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] SchedulingUpdateDto schedulingUpdateDto,int id)
    {
        _schedulingManager.Update(schedulingUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _schedulingManager.Delete(id);
        return Ok();
    }
    
}