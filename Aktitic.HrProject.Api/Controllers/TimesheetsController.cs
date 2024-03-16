using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;
using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController: ControllerBase
{
    private readonly ITimesheetManager _timesheetManager;

    public TimesheetsController(ITimesheetManager timesheetManager)
    {
        _timesheetManager = timesheetManager;
    }
    
    [HttpGet]
    public ActionResult<List<TimesheetReadDto>> GetAll()
    {
        return _timesheetManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TimesheetReadDto?> Get(int id)
    {
        var user = _timesheetManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] TimesheetAddDto timesheetAddDto)
    {
        _timesheetManager.Add(timesheetAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TimesheetUpdateDto timesheetUpdateDto,int id)
    {
        _timesheetManager.Update(timesheetUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _timesheetManager.Delete(id);
        return Ok();
    }
    
}