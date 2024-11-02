using Aktitic.HrProject.BL.Dtos.EmergencyContact;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactController(IEmergencyContactManager emergencyContact) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<EmergencyContactReadDto> GetAll(int userId)
    {
        return await emergencyContact.GetAll(userId);
    }
    
    
    [HttpPost("create")]
    public ActionResult<Task> Add(EmergencyContactAddDto emergencyContactAddDto)
    {
        var result =  emergencyContact.Add(emergencyContactAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update")]
    public ActionResult<Task> Update(EmergencyContactAddDto emergencyContactUpdateDto,int id)
    {
        var result= emergencyContact.Update(emergencyContactUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= emergencyContact.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
     

}