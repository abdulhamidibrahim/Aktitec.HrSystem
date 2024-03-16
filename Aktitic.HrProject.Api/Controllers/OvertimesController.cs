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
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] OvertimeAddDto overtimeAddDto)
    {
        _overtimeManager.Add(overtimeAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] OvertimeUpdateDto overtimeUpdateDto,int id)
    {
        _overtimeManager.Update(overtimeUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _overtimeManager.Delete(id);
        return Ok();
    }
    
}