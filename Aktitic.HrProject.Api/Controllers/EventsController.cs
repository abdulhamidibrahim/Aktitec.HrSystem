using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventManager eventManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Calendar),nameof(Roles.Read))]
    public Task<List<EventReadDto>> GetAll()
    {
        return eventManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Calendar),nameof(Roles.Read))]
    public ActionResult<EventReadDto?> Get(int id)
    {
        var result = eventManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Calendar),nameof(Roles.Add))]
    public ActionResult<EventReadDto> Add(EventAddDto eventAddDto)
    {
        var result = eventManager.Add(eventAddDto);
        if (result.Id == 0) return BadRequest("Failed to create");
        return Ok(result);
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Calendar),nameof(Roles.Edit))]
    public ActionResult<Task> Update(EventUpdateDto eventUpdateDto,int id)
    {
        var result= eventManager.Update(eventUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= eventManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }

    [HttpGet("getByMonth")]
    [AuthorizeRole(nameof(Pages.Estimate),nameof(Roles.Read))]
    public Task<List<EventReadDto>> GetByMonth([FromQuery]int month,[FromQuery] int year)
    {
        return  eventManager.GetByMonth(month, year);
    }

}