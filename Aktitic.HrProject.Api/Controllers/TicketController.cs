using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketManager ticketManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Read))]
    public Task<List<TicketReadDto>> GetAll()
    {
        return ticketManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Read))]
    public ActionResult<TicketReadDto?> Get(int id)
    {
        var result = ticketManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Add))]
    public ActionResult<Task> Add(TicketAddDto ticketAddDto)
    {
        var result = ticketManager.Add(ticketAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Edit))]
    public ActionResult<Task> Update(TicketUpdateDto ticketUpdateDto,int id)
    {
        var result= ticketManager.Update(ticketUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= ticketManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTickets")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Read))]
    public Task<FilteredTicketDto> GetFilteredTicketsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return ticketManager.GetFilteredTicketsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Tickets), nameof(Roles.Read))]
    public async Task<IEnumerable<TicketDto>> GlobalSearch(string search,string? column)
    {
        return await ticketManager.GlobalSearch(search,column);
    }


}