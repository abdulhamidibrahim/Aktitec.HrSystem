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
public class TerminationsController(ITerminationManager terminationManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Read))]
    public Task<List<TerminationReadDto>> GetAll()
    {
        return terminationManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Read))]
    public ActionResult<TerminationReadDto?> Get(int id)
    {
        var result = terminationManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Add))]
    public ActionResult<Task> Add( TerminationAddDto terminationsAddDto)
    {
        var result = terminationManager.Add(terminationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Edit))]
    public ActionResult<Task> Update( TerminationUpdateDto terminationsUpdateDto,int id)
    {
        var result= terminationManager.Update(terminationsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= terminationManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTerminations")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Read))]
    public Task<FilteredTerminationsDto> GetFilteredTerminationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return terminationManager.GetFilteredTerminationAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Termination), nameof(Roles.Read))]
    public async Task<IEnumerable<TerminationDto>> GlobalSearch(string search,string? column)
    {
        return await terminationManager.GlobalSearch(search,column);
    }


}