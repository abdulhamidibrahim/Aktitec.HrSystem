using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationSettingsController(INotificationSettingsManager notificationSettingsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<NotificationSettingsReadDto>>> GetAll()
    {
        var experiences = await notificationSettingsManager.GetAll();
        return Ok(experiences);
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<List<NotificationSettingsReadDto>>> AddCompanyNotification(NotificationSettingsAddDto dto)
    {
        var experiences = await notificationSettingsManager.Add(dto);
        if (experiences > 0) return BadRequest("Failed To Add");
        return Ok("success");
    }
    
    [HttpPut("Update")]
    public async Task<ActionResult<List<NotificationSettingsReadDto>>> Update
        (NotificationSettingsAddDto dto,int notificationSettingsId)
    {
        var experiences = await notificationSettingsManager.Update(dto,notificationSettingsId);
        if (experiences > 0) return BadRequest("Failed To Update");
        return Ok("Success");
    }
    
   
}