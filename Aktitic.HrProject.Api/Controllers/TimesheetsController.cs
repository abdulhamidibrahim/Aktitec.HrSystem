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
    
    [HttpPost]
    public ActionResult Add(TimesheetAddDto timesheetAddDto)
    {
        _timesheetManager.Add(timesheetAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(TimesheetUpdateDto timesheetUpdateDto)
    {
        _timesheetManager.Update(timesheetUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(TimesheetDeleteDto timesheetDeleteDto)
    {
        _timesheetManager.Delete(timesheetDeleteDto);
        return Ok();
    }
    
}