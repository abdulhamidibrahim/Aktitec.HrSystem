using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeavesController(ILeavesManager leaveManager) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<LeavesReadDto>> GetAll()
    {
        return leaveManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<LeavesReadDto?> Get(int id)
    {
        var result = leaveManager.Get(id);
        if (result == null) return NotFound("Leave not found.");
        return result;
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<LeavesGetFilteredDto>> GlobalSearch(string search,string? column)
    {
        return await leaveManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredLeaves")]
    public Task<FilteredLeavesDto> GetFilteredLeavesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return leaveManager.GetFilteredLeavesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }

    
    [HttpPost("create")]
    public ActionResult Add([FromForm] LeavesAddDto leaveAddDto)
    {
        var result=leaveManager.Add(leaveAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Leave added successfully.");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] LeavesUpdateDto leavesUpdateDto,int id)
    { 
        var result =leaveManager.Update(leavesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Leave updated successfully.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete( int id)
    {
        var result= leaveManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Leave deleted successfully.");
    }
    
}