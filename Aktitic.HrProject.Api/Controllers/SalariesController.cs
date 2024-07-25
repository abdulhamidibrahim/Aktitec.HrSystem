using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalariesController: ControllerBase
{
    private readonly ISalaryManager _salaryManager;

    public SalariesController(ISalaryManager salaryManager)
    {
        _salaryManager = salaryManager;
    }
    
    [HttpGet]
    public Task<List<SalaryReadDto>> GetAll()
    {
        return _salaryManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<SalaryReadDto?> Get(int id)
    {
        var result = _salaryManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(SalaryAddDto salaryAddDto)
    {
        var result = _salaryManager.Add(salaryAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(SalaryUpdateDto salaryUpdateDto,int id)
    {
        var result= _salaryManager.Update(salaryUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _salaryManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredSalaries")]
    public Task<FilteredSalariesDto> GetFilteredSalariesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _salaryManager.GetFilteredSalariesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<SalaryDto>> GlobalSearch(string search,string? column)
    {
        return await _salaryManager.GlobalSearch(search,column);
    }


}