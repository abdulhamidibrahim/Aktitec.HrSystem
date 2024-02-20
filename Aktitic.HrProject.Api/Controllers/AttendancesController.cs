using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendancesController: ControllerBase
{
    private readonly IAttendanceManager _attendanceManager;

    public AttendancesController(IAttendanceManager attendanceManager)
    {
        _attendanceManager = attendanceManager;
    }
    
    [HttpGet]
    public ActionResult<List<AttendanceReadDto>> GetAll()
    {
        return _attendanceManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<AttendanceReadDto?> Get(int id)
    {
        var user = _attendanceManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost]
    public ActionResult Add(AttendanceAddDto attendanceAddDto)
    {
        _attendanceManager.Add(attendanceAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(AttendanceUpdateDto attendanceUpdateDto)
    {
        _attendanceManager.Update(attendanceUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(AttendanceDeleteDto attendanceDeleteDto)
    {
        _attendanceManager.Delete(attendanceDeleteDto);
        return Ok();
    }
    
}