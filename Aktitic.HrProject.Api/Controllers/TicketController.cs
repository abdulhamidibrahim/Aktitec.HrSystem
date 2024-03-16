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
    public ActionResult<Task<TicketReadDto?>> Get(int id)
    {
        var user = _ticketManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] TicketAddDto ticketAddDto)
    {
        _ticketManager.Add(ticketAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] TicketUpdateDto ticketUpdateDto,int id)
    {
        _ticketManager.Update(ticketUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        _ticketManager.Delete(id);
        return Ok();
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