using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController: ControllerBase
{
    private readonly ITicketManager _ticketManager;

    public TicketController(ITicketManager ticketManager)
    {
        _ticketManager = ticketManager;
    }
    
    [HttpGet]
    public Task<List<TicketReadDto>> GetAll()
    {
        return _ticketManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TicketReadDto?> Get(int id)
    {
        var user = _ticketManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TicketAddDto ticketAddDto)
    {
        var result = _ticketManager.Add(ticketAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(TicketUpdateDto ticketUpdateDto,int id)
    {
        var result= _ticketManager.Update(ticketUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _ticketManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTickets")]
    public Task<FilteredTicketDto> GetFilteredTicketsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _ticketManager.GetFilteredTicketsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TicketDto>> GlobalSearch(string search,string? column)
    {
        return await _ticketManager.GlobalSearch(search,column);
    }


}