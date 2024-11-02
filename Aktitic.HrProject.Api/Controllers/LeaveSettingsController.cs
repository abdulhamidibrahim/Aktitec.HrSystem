using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveSettingsController(ILeaveSettingsManager leaveSettingManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.LeaveSettings),nameof(Roles.Read))]
    public ActionResult<List<LeaveSettingReadDto>> GetAll()
    {
        return leaveSettingManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.LeaveSettings),nameof(Roles.Read))]
    public ActionResult<LeaveSettingReadDto?> Get(int id)
    {
        var readDto = leaveSettingManager.Get(id);
        if (readDto == null) return NotFound("Leave setting not found.");
        return Ok(readDto);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.LeaveSettings),nameof(Roles.Add))]
    public ActionResult Add([FromBody] LeaveSettingAddDto leaveSettingAddDto)
    {
        var result= leaveSettingManager.Add(leaveSettingAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Leave setting added.");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.LeaveSettings),nameof(Roles.Edit))]
    public ActionResult Update([FromBody] LeaveSettingUpdateDto leaveSettingUpdateDto,int id)
    {
        var result =leaveSettingManager.Update(leaveSettingUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Leave setting updated.");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.LeaveSettings),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    {
        var result = leaveSettingManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Leave setting deleted.");
    }
    
}