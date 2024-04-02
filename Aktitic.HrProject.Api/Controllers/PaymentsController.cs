using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController: ControllerBase
{
    private readonly IPaymentManager _paymentManager;

    public PaymentsController(IPaymentManager paymentManager)
    {
        _paymentManager = paymentManager;
    }
    
    [HttpGet]
    public Task<List<PaymentReadDto>> GetAll()
    {
        return _paymentManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PaymentReadDto?> Get(int id)
    {
        var user = _paymentManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(PaymentAddDto paymentAddDto)
    {
        var result = _paymentManager.Add(paymentAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(PaymentUpdateDto paymentUpdateDto,int id)
    {
        var result= _paymentManager.Update(paymentUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _paymentManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPayments")]
    public Task<FilteredPaymentDto> GetFilteredPaymentsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _paymentManager.GetFilteredPaymentsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PaymentDto>> GlobalSearch(string search,string? column)
    {
        return await _paymentManager.GlobalSearch(search,column);
    }


}