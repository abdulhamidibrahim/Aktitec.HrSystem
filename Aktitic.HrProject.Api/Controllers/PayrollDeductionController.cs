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
public class PayrollDeductionController(IPayrollDeductionManager payrollDeductionManager) : ControllerBase
{
    [HttpGet]
    public Task<List<PayrollDeductionReadDto>> GetAll()
    {
        return payrollDeductionManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PayrollDeductionReadDto?> Get(int id)
    {
        var result = payrollDeductionManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PayrollDeductionAddDto payrollDeductionAddDto)
    {
        var result = payrollDeductionManager.Add(payrollDeductionAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PayrollDeductionUpdateDto payrollDeductionUpdateDto,int id)
    {
        var result= payrollDeductionManager.Update(payrollDeductionUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= payrollDeductionManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPayrollDeduction")]
    public Task<FilteredPayrollDeductionsDto> GetFilteredPayrollDeductionAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return payrollDeductionManager.GetFilteredPayrollDeductionsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PayrollDeductionDto>> GlobalSearch(string search,string? column)
    {
        return await payrollDeductionManager.GlobalSearch(search,column);
    }


}