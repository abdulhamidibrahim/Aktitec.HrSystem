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
public class ContactsController(IContactsManager contactManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Read))]
    public Task<List<ContactReadDto>> GetAll()
    {
        return contactManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Read))]
    public ActionResult<ContactReadDto?> Get(int id)
    {
        var result = contactManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Add))]
    public ActionResult<Task> Add([FromForm] ContactAddDto contactAddDto)
    {
        var result = contactManager.Add(contactAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Edit))]
    public ActionResult<Task> Update([FromForm] ContactUpdateDto contactUpdateDto,int id)
    {
        var result= contactManager.Update(contactUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= contactManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Read))]
    public async Task<IEnumerable<ContactDto>> GlobalSearch(string search,string? column)
    {
        return await contactManager.GlobalSearch(search,column);
    }

    [HttpGet("getByType")]
    [AuthorizeRole(nameof(Pages.Contacts),nameof(Roles.Read))]
    public async Task<List<ContactReadDto>> GetByType(string type)
    {
        return await contactManager.GetByType(type);
    }

}