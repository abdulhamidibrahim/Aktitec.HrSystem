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
public class ResignationsController(IResignationManager resignationManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public Task<List<ResignationReadDto>> GetAll()
    {
        return resignationManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public ActionResult<ResignationReadDto?> Get(int id)
    {
        var result = resignationManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]   
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Add))]
    public ActionResult<Task> Add( ResignationAddDto resignationsAddDto)
    {
        var result = resignationManager.Add(resignationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Edit))]
    public ActionResult<Task> Update( ResignationUpdateDto resignationsUpdateDto,int id)
    {
        var result= resignationManager.Update(resignationsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= resignationManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredResignations")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public Task<FilteredResignationsDto> GetFilteredResignationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return resignationManager.GetFilteredResignationAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public async Task<IEnumerable<ResignationDto>> GlobalSearch(string search,string? column)
    {
        return await resignationManager.GlobalSearch(search,column);
    }


}