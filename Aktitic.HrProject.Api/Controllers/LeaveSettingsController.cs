using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveSettingsController: ControllerBase
{
    private readonly ILeaveSettingsManager _leaveSettingManager;

    public LeaveSettingsController(ILeaveSettingsManager leaveSettingManager)
    {
        _leaveSettingManager = leaveSettingManager;
    }
    
    [HttpGet]
    public ActionResult<List<LeaveSettingReadDto>> GetAll()
    {
        return _leaveSettingManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<LeaveSettingReadDto?> Get(int id)
    {
        var readDto = _leaveSettingManager.Get(id);
        if (readDto == null) return NotFound("Leave setting not found.");
        return Ok(readDto);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] LeaveSettingAddDto leaveSettingAddDto)
    {
        _leaveSettingManager.Add(leaveSettingAddDto);
        return Ok("Leave setting added.");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] LeaveSettingUpdateDto leaveSettingUpdateDto,int id)
    {
        _leaveSettingManager.Update(leaveSettingUpdateDto,id);
        return Ok("Leave setting updated.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _leaveSettingManager.Delete(id);
        return Ok("Leave setting deleted.");
    }
    
}