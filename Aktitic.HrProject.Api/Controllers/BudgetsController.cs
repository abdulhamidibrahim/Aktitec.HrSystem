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
public class BudgetsController(IBudgetManager budgetManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Read))]
    public Task<List<BudgetReadDto>> GetAll()
    {
        return budgetManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Read))]
    public ActionResult<BudgetReadDto?> Get(int id)
    {
        var result = budgetManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Add))]
    public ActionResult<Task> Add(BudgetAddDto budgetAddDto)
    {
        var result = budgetManager.Add(budgetAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Edit))]
    public ActionResult<Task> Update(BudgetUpdateDto budgetUpdateDto,int id)
    {
        var result= budgetManager.Update(budgetUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var budget = budgetManager.Get(id);
        if (budget == null)
        {
            return NotFound();
        }
        budgetManager.Delete(id);
        return StatusCode(200, "Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgets")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Read))]
    public Task<FilteredBudgetDto> GetFilteredBudgetsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return budgetManager.GetFilteredBudgetsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Budgets),nameof(Roles.Read))]
    public async Task<IEnumerable<BudgetDto>> GlobalSearch(string search,string? column)
    {
        return await budgetManager.GlobalSearch(search,column);
    }


}