using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OvertimesController: ControllerBase
{
    private readonly IOvertimeManager _overtimeManager;

    public OvertimesController(IOvertimeManager overtimeManager)
    {
        _overtimeManager = overtimeManager;
    }
    
    [HttpGet]
    public ActionResult<List<OvertimeReadDto>> GetAll()
    {
        return _overtimeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<OvertimeReadDto?> Get(int id)
    {
        var user = _overtimeManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost]
    public ActionResult Add(OvertimeAddDto overtimeAddDto)
    {
        _overtimeManager.Add(overtimeAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(OvertimeUpdateDto overtimeUpdateDto)
    {
        _overtimeManager.Update(overtimeUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(OvertimeDeleteDto overtimeDeleteDto)
    {
        _overtimeManager.Delete(overtimeDeleteDto);
        return Ok();
    }
    
}