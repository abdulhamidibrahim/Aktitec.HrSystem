using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController(IExpensesManager expensesManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Read))]
    public Task<List<ExpensesReadDto>> GetAll()
    {
        return expensesManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Read))]
    public ActionResult<ExpensesReadDto?> Get(int id)
    {
        var result = expensesManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Add))]
    public ActionResult<Task> Add(ExpensesAddDto expensesAddDto)
    {
        var result = expensesManager.Add(expensesAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Edit))]
    public ActionResult<Task> Update(ExpensesUpdateDto expensesUpdateDto,int id)
    {
        var result= expensesManager.Update(expensesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= expensesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
    
    [HttpGet("getFilteredExpenses")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Read))]
    public Task<FilteredExpensesDto> GetFilteredExpensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return expensesManager.GetFilteredExpensesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Expenses),nameof(Roles.Read))]
    public async Task<IEnumerable<ExpensesDto>> GlobalSearch(string search,string? column)
    {
        return await expensesManager.GlobalSearch(search,column);
    }


}