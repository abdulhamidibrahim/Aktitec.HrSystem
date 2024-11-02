using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesignationsController(IDesignationManager designationManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Read))]
    public async Task<ActionResult<List<DesignationReadDto>>> GetAll()
    {
        var result = await designationManager.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Read))]
    public ActionResult<DesignationReadDto?> Get(int id)
    {
        var designation =  designationManager.Get(id);

        if (designation == null)
        {
             NotFound("DesignationId Not Found!");
        }
        return Ok(designation);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Add))]
    public ActionResult Add([FromForm] DesignationAddDto designationAddDto)
    {
        var result =designationManager.Add(designationAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("DesignationId Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Edit))]
    public ActionResult Update([FromForm] DesignationUpdateDto designationUpdateDto,int id)
    {
        var result = designationManager.Update(designationUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("DesignationId Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    {
         var result = designationManager.Delete(id);
         if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("DesignationId Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Read))]
    public async Task<IEnumerable<DesignationDto>> GlobalSearch(string search,string? column)
    {
        return await designationManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredDesignations")]
    [AuthorizeRole(nameof(Pages.Designations),nameof(Roles.Read))]
    public Task<FilteredDesignationDto> GetFilteredDesignationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return designationManager.GetFilteredDesignationsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }

}