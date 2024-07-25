using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResignationsController: ControllerBase
{
    private readonly IResignationManager _resignationsManager;

    public ResignationsController(IResignationManager resignationManager)
    {
        _resignationsManager = resignationManager;
    }
    
    [HttpGet]
    public Task<List<ResignationReadDto>> GetAll()
    {
        return _resignationsManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ResignationReadDto?> Get(int id)
    {
        var result = _resignationsManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add( ResignationAddDto resignationsAddDto)
    {
        var result = _resignationsManager.Add(resignationsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update( ResignationUpdateDto resignationsUpdateDto,int id)
    {
        var result= _resignationsManager.Update(resignationsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _resignationsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredResignations")]
    public Task<FilteredResignationsDto> GetFilteredResignationsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _resignationsManager.GetFilteredResignationAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ResignationDto>> GlobalSearch(string search,string? column)
    {
        return await _resignationsManager.GlobalSearch(search,column);
    }


}