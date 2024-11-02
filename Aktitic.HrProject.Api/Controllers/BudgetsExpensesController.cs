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
public class BudgetsExpensesController(IBudgetExpensesManager budgetsExpensesManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Read))]
    public Task<List<BudgetExpensesReadDto>> GetAll()
    {
        return budgetsExpensesManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Read))]
    public ActionResult<BudgetExpensesReadDto?> Get(int id)
    {
        var result = budgetsExpensesManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Add))]
    public ActionResult<Task> Add(BudgetExpensesAddDto budgetsExpensesAddDto)
    {
        var result = budgetsExpensesManager.Add(budgetsExpensesAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Edit))]
    public ActionResult<Task> Update(BudgetExpensesUpdateDto budgetsExpensesUpdateDto,int id)
    {
        var result= budgetsExpensesManager.Update(budgetsExpensesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= budgetsExpensesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgetsExpenses")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Read))]
    public Task<FilteredBudgetExpensesDto> GetFilteredBudgetsExpensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return budgetsExpensesManager.GetFilteredExpensesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.BudgetsExpenses),nameof(Roles.Read))]
    public async Task<IEnumerable<BudgetExpensesSearchDto>> GlobalSearch(string search,string? column)
    {
        return await budgetsExpensesManager.GlobalSearch(search,column);
    }


}