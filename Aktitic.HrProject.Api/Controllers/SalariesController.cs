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
public class SalariesController(ISalaryManager salaryManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.EmployeeSalary), nameof(Roles.Read))]
    public Task<List<SalaryReadDto>> GetAll()
    {
        return salaryManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public ActionResult<SalaryReadDto?> Get(int id)
    {
        var result = salaryManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Add))]
    public ActionResult<Task> Add(SalaryAddDto salaryAddDto)
    {
        var result = salaryManager.Add(salaryAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Edit))]
    public ActionResult<Task> Update(SalaryUpdateDto salaryUpdateDto,int id)
    {
        var result= salaryManager.Update(salaryUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= salaryManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredSalaries")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public Task<FilteredSalariesDto> GetFilteredSalariesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return salaryManager.GetFilteredSalariesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Resignation), nameof(Roles.Read))]
    public async Task<IEnumerable<SalaryDto>> GlobalSearch(string search,string? column)
    {
        return await salaryManager.GlobalSearch(search,column);
    }


}