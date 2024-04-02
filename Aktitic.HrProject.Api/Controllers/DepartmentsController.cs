using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController: ControllerBase
{
    private readonly IDepartmentManager _departmentManager;

    public DepartmentsController(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }
    
    [HttpGet]
    public ActionResult<List<DepartmentReadDto>> GetAll()
    {
        return _departmentManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<DepartmentReadDto?> Get(int id)
    {
        var user = _departmentManager.Get(id);
        if (user == null) return NotFound("Not Found!");
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] DepartmentAddDto departmentAddDto)
    {
         var result = _departmentManager.Add(departmentAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok("Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] DepartmentUpdateDto departmentUpdateDto,int id)
    {
        var result  =_departmentManager.Update(departmentUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _departmentManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<DepartmentDto>> GlobalSearch(string search,string? column)
    {
        return await _departmentManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredDepartments")]
    public Task<FilteredDepartmentDto> GetFilteredDepartmentsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _departmentManager.GetFilteredDepartmentsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}