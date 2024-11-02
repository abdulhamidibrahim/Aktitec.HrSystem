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
public class PaymentsController(IPaymentManager paymentManager) : ControllerBase
{
    [HttpGet]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Read))]
    public Task<List<PaymentReadDto>> GetAll()
    {
        return paymentManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Read))]
    public ActionResult<PaymentReadDto?> Get(int id)
    {
        var result = paymentManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Add))]
    public ActionResult<Task> Add(PaymentAddDto paymentAddDto)
    {
        var result = paymentManager.Add(paymentAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Edit))]
    public ActionResult<Task> Update(PaymentUpdateDto paymentUpdateDto,int id)
    {
        var result= paymentManager.Update(paymentUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= paymentManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPayments")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Read))]
    public Task<FilteredPaymentDto> GetFilteredPaymentsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return paymentManager.GetFilteredPaymentsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [HttpGet, AuthorizeRole(nameof(Pages.Payments), nameof(Roles.Read))]
    public async Task<IEnumerable<PaymentDto>> GlobalSearch(string search,string? column)
    {
        return await paymentManager.GlobalSearch(search,column);
    }


}