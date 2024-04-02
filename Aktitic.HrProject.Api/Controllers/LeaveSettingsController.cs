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
        var result= _leaveSettingManager.Add(leaveSettingAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Leave setting added.");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] LeaveSettingUpdateDto leaveSettingUpdateDto,int id)
    {
        var result =_leaveSettingManager.Update(leaveSettingUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Leave setting updated.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _leaveSettingManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Leave setting deleted.");
    }
    
}