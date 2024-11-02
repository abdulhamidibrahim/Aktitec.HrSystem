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
public class BudgetsRevenuesController(IBudgetRevenuesManager categoryManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Read))]
    public Task<List<BudgetRevenuesReadDto>> GetAll()
    {
        return categoryManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Read))]
    public ActionResult<BudgetRevenuesReadDto?> Get(int id)
    {
        var result = categoryManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Add))]
    public ActionResult<Task> Add(BudgetRevenuesAddDto categoryAddDto)
    {
        var result = categoryManager.Add(categoryAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Edit))]
    public ActionResult<Task> Update(BudgetRevenuesUpdateDto categoryUpdateDto,int id)
    {
        var result= categoryManager.Update(categoryUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= categoryManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredBudgetsRevenues")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Read))]
    public Task<FilteredBudgetRevenuesDto> GetFilteredBudgetsRevenuesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return categoryManager.GetFilteredRevenuesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.BudgetsRevenues),nameof(Roles.Read))]
    public async Task<IEnumerable<BudgetRevenuesSearchDto>> GlobalSearch(string search,string? column)
    {
        return await categoryManager.GlobalSearch(search,column);
    }


}