using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController: ControllerBase
{
    private readonly IBudgetManager _budgetManager;

    public BudgetsController(IBudgetManager budgetManager)
    {
        _budgetManager = budgetManager;
    }
    
    [HttpGet]
    public Task<List<BudgetReadDto>> GetAll()
    {
        return _budgetManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<BudgetReadDto?> Get(int id)
    {
        var user = _budgetManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(BudgetAddDto budgetAddDto)
    {
        var result = _budgetManager.Add(budgetAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(BudgetUpdateDto budgetUpdateDto,int id)
    {
        var result= _budgetManager.Update(budgetUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _budgetManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgets")]
    public Task<FilteredBudgetDto> GetFilteredBudgetsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _budgetManager.GetFilteredBudgetsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<BudgetDto>> GlobalSearch(string search,string? column)
    {
        return await _budgetManager.GlobalSearch(search,column);
    }


}