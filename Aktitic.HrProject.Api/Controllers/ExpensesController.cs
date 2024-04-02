using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController: ControllerBase
{
    private readonly IExpensesManager _expensesManager;

    public ExpensesController(IExpensesManager expensesManager)
    {
        _expensesManager = expensesManager;
    }
    
    [HttpGet]
    public Task<List<ExpensesReadDto>> GetAll()
    {
        return _expensesManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ExpensesReadDto?> Get(int id)
    {
        var user = _expensesManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(ExpensesAddDto expensesAddDto)
    {
        var result = _expensesManager.Add(expensesAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(ExpensesUpdateDto expensesUpdateDto,int id)
    {
        var result= _expensesManager.Update(expensesUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _expensesManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredExpenses")]
    public Task<FilteredExpensesDto> GetFilteredExpensesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _expensesManager.GetFilteredExpensesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ExpensesDto>> GlobalSearch(string search,string? column)
    {
        return await _expensesManager.GlobalSearch(search,column);
    }


}