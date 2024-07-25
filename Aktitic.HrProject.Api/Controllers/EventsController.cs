using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController: ControllerBase
{
    private readonly IEventManager _taxManager;

    public EventsController(IEventManager taxManager)
    {
        _taxManager = taxManager;
    }
    
    [HttpGet]
    public Task<List<EventReadDto>> GetAll()
    {
        return _taxManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<EventReadDto?> Get(int id)
    {
        var result = _taxManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<EventReadDto> Add(EventAddDto taxAddDto)
    {
        var result = _taxManager.Add(taxAddDto);
        if (result.Id == 0) return BadRequest("Failed to create");
        return Ok(result);
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(EventUpdateDto taxUpdateDto,int id)
    {
        var result= _taxManager.Update(taxUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _taxManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }

    [HttpGet("getByMonth")]
    public Task<List<EventReadDto>> GetByMonth([FromQuery]int month,[FromQuery] int year)
    {
        return  _taxManager.GetByMonth(month, year);
    }

}