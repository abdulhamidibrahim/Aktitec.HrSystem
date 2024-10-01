using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(IContactsManager contactManager) : ControllerBase
{
    [HttpGet]
    public Task<List<ContactReadDto>> GetAll()
    {
        return contactManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ContactReadDto?> Get(int id)
    {
        var result = contactManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add([FromForm] ContactAddDto contactAddDto)
    {
        var result = contactManager.Add(contactAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update([FromForm] ContactUpdateDto contactUpdateDto,int id)
    {
        var result= contactManager.Update(contactUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= contactManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ContactDto>> GlobalSearch(string search,string? column)
    {
        return await contactManager.GlobalSearch(search,column);
    }

    [HttpGet("getByType")]
    public async Task<List<ContactReadDto>> GetByType(string type)
    {
        return await contactManager.GetByType(type);
    }

}