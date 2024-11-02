using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayrollAdditionController(IPayrollAdditionManager payrollAdditionManager) : ControllerBase
{
    [HttpGet]
    public Task<List<PayrollAdditionReadDto>> GetAll()
    {
        return payrollAdditionManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PayrollAdditionReadDto?> Get(int id)
    {
        var result = payrollAdditionManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PayrollAdditionAddDto payrollAdditionAddDto)
    {
        var result = payrollAdditionManager.Add(payrollAdditionAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PayrollAdditionUpdateDto payrollAdditionUpdateDto,int id)
    {
        var result= payrollAdditionManager.Update(payrollAdditionUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= payrollAdditionManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPayrollAddition")]
    public Task<FilteredPayrollAdditionsDto> GetFilteredPayrollAdditionAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return payrollAdditionManager.GetFilteredPayrollAdditionsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PayrollAdditionDto>> GlobalSearch(string search,string? column)
    {
        return await payrollAdditionManager.GlobalSearch(search,column);
    }


}