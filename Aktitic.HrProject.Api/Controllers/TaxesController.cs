using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxesController: ControllerBase
{
    private readonly ITaxManager _taxManager;

    public TaxesController(ITaxManager taxManager)
    {
        _taxManager = taxManager;
    }
    
    [HttpGet]
    public Task<List<TaxReadDto>> GetAll()
    {
        return _taxManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TaxReadDto?> Get(int id)
    {
        var user = _taxManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TaxAddDto taxAddDto)
    {
        var result = _taxManager.Add(taxAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(TaxUpdateDto taxUpdateDto,int id)
    {
        var result= _taxManager.Update(taxUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _taxManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTaxes")]
    public Task<FilteredTaxDto> GetFilteredTaxesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _taxManager.GetFilteredTaxsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TaxDto>> GlobalSearch(string search,string? column)
    {
        return await _taxManager.GlobalSearch(search,column);
    }


}