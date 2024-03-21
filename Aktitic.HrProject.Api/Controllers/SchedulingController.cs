using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulingController: ControllerBase
{
    private readonly ISchedulingManager _schedulingManager;

    public SchedulingController(ISchedulingManager schedulingManager)
    {
        _schedulingManager = schedulingManager;
    }
    
    [HttpGet]
    public async Task<List<SchedulingReadDto>> GetAll()
    {
        return await _schedulingManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<SchedulingReadDto?> Get(int id)
    {
        var schedule = _schedulingManager.Get(id);
        if (schedule == null) return NotFound("Schedule not found");
        return Ok(schedule);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Add([FromBody] SchedulingAddDto schedulingAddDto)
    {
        var result = await _schedulingManager.Add(schedulingAddDto);
        if (result == 0) return BadRequest("Failed to create");
        return Ok("Created Successfully");
    }
    
    [HttpPut("update/{id}")]
    public async Task<ActionResult> Update([FromBody] SchedulingUpdateDto schedulingUpdateDto,int id)
    {
        var result =await _schedulingManager.Update(schedulingUpdateDto,id);
        if (result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _schedulingManager.Delete(id);
        if (result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    [HttpGet("GetAllEmployeesScheduling")]
    public async Task<List<FilteredSchedulingDto>> GetAllEmployeesScheduling(int page, int pageSize)
    {
        return await _schedulingManager.GetAllEmployeesScheduling(page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ScheduleDto>> GlobalSearch(string search,string? column)
    {
        return await _schedulingManager.GlobalSearch(search,column);
    }
}