using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsRevenuesController: ControllerBase
{
    private readonly IBudgetRevenuesManager _categoryManager;

    public BudgetsRevenuesController(IBudgetRevenuesManager categoryManager)
    {
        _categoryManager = categoryManager;
    }
    
    [HttpGet]
    public Task<List<BudgetRevenuesReadDto>> GetAll()
    {
        return _categoryManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<BudgetRevenuesReadDto?> Get(int id)
    {
        var result = _categoryManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(BudgetRevenuesAddDto categoryAddDto)
    {
        var result = _categoryManager.Add(categoryAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(BudgetRevenuesUpdateDto categoryUpdateDto,int id)
    {
        var result= _categoryManager.Update(categoryUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _categoryManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgetsRevenues")]
    public Task<FilteredBudgetRevenuesDto> GetFilteredBudgetsRevenuesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _categoryManager.GetFilteredRevenuesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<BudgetRevenuesSearchDto>> GlobalSearch(string search,string? column)
    {
        return await _categoryManager.GlobalSearch(search,column);
    }


}