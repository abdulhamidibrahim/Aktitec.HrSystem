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
public class PayrollOvertimeController: ControllerBase
{
    private readonly IPayrollOvertimeManager _payrollOvertimeManager;

    public PayrollOvertimeController(IPayrollOvertimeManager payrollOvertimeManager)
    {
        _payrollOvertimeManager = payrollOvertimeManager;
    }
    
    [HttpGet]
    public Task<List<PayrollOvertimeReadDto>> GetAll()
    {
        return _payrollOvertimeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PayrollOvertimeReadDto?> Get(int id)
    {
        var result = _payrollOvertimeManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PayrollOvertimeAddDto payrollOvertimeAddDto)
    {
        var result = _payrollOvertimeManager.Add(payrollOvertimeAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PayrollOvertimeUpdateDto payrollOvertimeUpdateDto,int id)
    {
        var result= _payrollOvertimeManager.Update(payrollOvertimeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _payrollOvertimeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPayrollOvertime")]
    public Task<FilteredPayrollOvertimesDto> GetFilteredPayrollOvertimeAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _payrollOvertimeManager.GetFilteredPayrollOvertimesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PayrollOvertimeDto>> GlobalSearch(string search,string? column)
    {
        return await _payrollOvertimeManager.GlobalSearch(search,column);
    }


}