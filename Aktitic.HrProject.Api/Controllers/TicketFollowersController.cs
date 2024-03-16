using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketFollowersController: ControllerBase
{
    private readonly ITicketFollowersManager _ticketFollowersManager;

    public TicketFollowersController(ITicketFollowersManager ticketFollowersManager)
    {
        _ticketFollowersManager = ticketFollowersManager;
    }
    
    [HttpGet]
    public Task<List<TicketFollowersReadDto>> GetAll()
    {
        return _ticketFollowersManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Task<TicketFollowersReadDto?>> Get(int id)
    {
        var user = _ticketFollowersManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TicketFollowersAddDto ticketFollowersAddDto)
    {
        _ticketFollowersManager.Add(ticketFollowersAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TicketFollowersUpdateDto ticketFollowersUpdateDto,int id)
    {
        _ticketFollowersManager.Update(ticketFollowersUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        _ticketFollowersManager.Delete(id);
        return Ok();
    }
    
}