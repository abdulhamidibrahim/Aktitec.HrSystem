using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendancesController: ControllerBase
{
    private readonly IAttendanceManager _attendanceManager;

    public AttendancesController(IAttendanceManager attendanceManager)
    {
        _attendanceManager = attendanceManager;
    }
    
    [HttpGet]
    public ActionResult<List<AttendanceReadDto>> GetAll()
    {
        return _attendanceManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<AttendanceReadDto?> Get(int id)
    {
        var user = _attendanceManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] AttendanceAddDto attendanceAddDto)
    {
        var result = _attendanceManager.Add(attendanceAddDto);
        if (result.Result == 0) return BadRequest("Failed to Create!");
        return Ok("Create Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] AttendanceUpdateDto attendanceUpdateDto,int id)
    {
        var result = _attendanceManager.Update(attendanceUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to Update!");
        return Ok("Updated successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _attendanceManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete!");
        return Ok("Deleted successfully!");
    }
    
    [HttpGet("getFilteredAttendances")]
    public Task<FilteredAttendanceDto> GetFilteredAttendancesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _attendanceManager.GetFilteredAttendancesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<AttendanceDto>> GlobalSearch(string search,string? column)
    {
        return await _attendanceManager.GlobalSearch(search,column);
    }


    [HttpGet("getAllAttendances")]
    public async Task<List<EmployeeAttendanceDto>> GetAllAttendancesAsync()
    {
        return  await _attendanceManager.GetAllEmployeeAttendanceInCurrentMonth();
    }

}