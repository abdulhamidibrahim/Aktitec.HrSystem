using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(IDepartmentManager departmentManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Read))]
    public ActionResult<List<DepartmentReadDto>> GetAll()
    {
        return departmentManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Read))]
    public ActionResult<DepartmentReadDto?> Get(int id)
    
    {
        var result = departmentManager.Get(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Add))]
    public ActionResult Add([FromForm] DepartmentAddDto departmentAddDto)
    {
         var result = departmentManager.Add(departmentAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok("Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Edit))]
    public ActionResult Update([FromForm] DepartmentUpdateDto departmentUpdateDto,int id)
    {
        var result  =departmentManager.Update(departmentUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    {
        var result = departmentManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Read))]
    public async Task<IEnumerable<DepartmentDto>> GlobalSearch(string search,string? column)
    {
        return await departmentManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredDepartments")]
    [AuthorizeRole(nameof(Pages.Departments),nameof(Roles.Read))]
    public Task<FilteredDepartmentDto> GetFilteredDepartmentsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return departmentManager.GetFilteredDepartmentsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}