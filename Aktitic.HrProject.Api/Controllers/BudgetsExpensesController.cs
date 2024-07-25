using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsExpensesController: ControllerBase
{
    private readonly IBudgetExpensesManager _budgetsExpensesManager;

    public BudgetsExpensesController(IBudgetExpensesManager budgetsExpensesManager)
    {
        _budgetsExpensesManager = budgetsExpensesManager;
    }
    
    [HttpGet]
    public Task<List<BudgetExpensesReadDto>> GetAll()
    {
        return _budgetsExpensesManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<BudgetExpensesReadDto?> Get(int id)
    {
        var result = _budgetsExpensesManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(BudgetExpensesAddDto budgetsExpensesAddDto)
    {
        var result = _budgetsExpensesManager.Add(budgetsExpensesAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(BudgetExpensesUpdateDto budgetsExpensesUpdateDto,int id)
    {
        var result= _budgetsExpensesManager.Update(budgetsExpensesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _budgetsExpensesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgetsExpenses")]
    public Task<FilteredBudgetExpensesDto> GetFilteredBudgetsExpensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _budgetsExpensesManager.GetFilteredExpensesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<BudgetExpensesSearchDto>> GlobalSearch(string search,string? column)
    {
        return await _budgetsExpensesManager.GlobalSearch(search,column);
    }


}