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
    
    [HttpPost]
    public ActionResult Add(SchedulingAddDto schedulingAddDto)
    {
        _schedulingManager.Add(schedulingAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(SchedulingUpdateDto schedulingUpdateDto)
    {
        _schedulingManager.Update(schedulingUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(SchedulingDeleteDto schedulingDeleteDto)
    {
        _schedulingManager.Delete(schedulingDeleteDto);
        return Ok();
    }
    
}