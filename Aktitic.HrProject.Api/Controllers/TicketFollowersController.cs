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
        var result =_ticketFollowersManager.Add(ticketFollowersAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("TicketFollower added");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TicketFollowersUpdateDto ticketFollowersUpdateDto,int id)
    {
        var result =_ticketFollowersManager.Update(ticketFollowersUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("TicketFollower updated");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result =_ticketFollowersManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("TicketFollower deleted");
    }
    
}