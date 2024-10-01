using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(INotificationManager notificationsManager) : ControllerBase
{
    [HttpGet("GetReceivedNotifications/{id}")]
    public ActionResult<NotificationReadDto?> Get(int id)
    {
        var result = notificationsManager.GetReceivedNotifications(id);
        // if (result == null) return NotFound();
        return result;
    }
    
    [HttpPost("FireNotification")]
    public ActionResult Add([FromBody] NotificationAddDto notificationsAddDto)
    {
        var result =notificationsManager.AddGeneral(notificationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        notificationsManager.SendNotification(notificationsAddDto.Content);
        return Ok("General Notification added!");
    }

    [HttpPost("FireNotificationForCompany/{id}")]
    public ActionResult AddForCompany(int id, NotificationAddDto notificationsAddDto)
    {
        var result = notificationsManager.AddForCompany(notificationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        notificationsManager.SendNotificationToSpecificCompany(id,notificationsAddDto.Content);
        return Ok("Company Notification added!"); 
    }
    
    [HttpPost("FireNotificationForUsers")]
    public ActionResult AddForUsers( NotificationAddDto notificationsAddDto)
    {
        var result = notificationsManager.AddForCompany(notificationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        if (notificationsAddDto.Receivers != null) return BadRequest("Enter the message Receivers");
        notificationsManager.SendNotificationToSpecificUsers(notificationsAddDto.Receivers, notificationsAddDto.Content);
        return Ok("Specific Users Notification added!"); 
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result= notificationsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Notifications deleted!");
    }
    
    
   
}