using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulingController(ISchedulingManager schedulingManager) : ControllerBase
{
    [HttpGet]
    public async Task<List<SchedulingReadDto>> GetAll()
    {
        return await schedulingManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<SchedulingReadDto?> Get(int id)
    {
        var schedule = schedulingManager.Get(id);
        if (schedule == null) return NotFound("Schedule not found");
        return Ok(schedule);
    }
    
    [HttpPost("create")]
    public  ActionResult Add([FromBody] SchedulingAddDto schedulingAddDto)
    {
        var result= schedulingManager.Add(schedulingAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    public  ActionResult Update([FromBody] SchedulingUpdateDto schedulingUpdateDto,int id)
    { 
        var result=schedulingManager.Update(schedulingUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =schedulingManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    // [HttpGet("GetAllEmployeesScheduling")]
    // public ActionResult<List<FilteredSchedulingDto>> GetAllEmployeesScheduling(int page, int pageSize)
    // {
    //     var result =  _schedulingManager.GetAllEmployeesScheduling(page,pageSize);
    //     return Ok(result);
    // }
    [HttpGet("GetAllEmployeesScheduling")]
    public  ActionResult<List<FilteredSchedulingDto>> GetAllEmployeesScheduling(int page, int pageSize,DateOnly? startDate)
    {
        if(startDate== DateOnly.MinValue)
            return BadRequest("Invalid Date");
        if (startDate == null)
        {
            var result = schedulingManager.GetAllEmployeesScheduling(page, pageSize);
            return Ok(result);
        }
        else
        {
            var result = schedulingManager.GetAllEmployeesScheduling(page, pageSize, startDate);
            return Ok(result);
        }
        
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ScheduleDto>> GlobalSearch(string search,string? column)
    {
        return await schedulingManager.GlobalSearch(search,column);
    }
}