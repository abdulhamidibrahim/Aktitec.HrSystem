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
public class TaxesController(ITaxManager taxManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Read))]
    public Task<List<TaxReadDto>> GetAll()
    {
        return taxManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Read))]
    public ActionResult<TaxReadDto?> Get(int id)
    {
        var result = taxManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Add))]
    public ActionResult<Task> Add(TaxAddDto taxAddDto)
    {
        var result = taxManager.Add(taxAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Edit))]
    public ActionResult<Task> Update(TaxUpdateDto taxUpdateDto,int id)
    {
        var result= taxManager.Update(taxUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= taxManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTaxes")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Read))]
    public Task<FilteredTaxDto> GetFilteredTaxesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return taxManager.GetFilteredTaxsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Taxes), nameof(Roles.Read))]
    public async Task<IEnumerable<TaxDto>> GlobalSearch(string search,string? column)
    {
        return await taxManager.GlobalSearch(search,column);
    }


}