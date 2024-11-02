using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController(IInvoiceManager invoiceManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Read))]
    public Task<List<InvoiceReadDto>> GetAll()
    {
        return invoiceManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Read))]
    public ActionResult<InvoiceReadDto?> Get(int id)
    {
        var result = invoiceManager.Get(id);
        // if (result == null) return BadRequest("No invoice found");
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Add))]
    public ActionResult<Task> Add(InvoiceAddDto invoiceAddDto)
    {
        var result = invoiceManager.Add(invoiceAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Edit))]
    public ActionResult<Task> Update(InvoiceUpdateDto invoiceUpdateDto,int id)
    {
        var result= invoiceManager.Update(invoiceUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= invoiceManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredInvoices")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Read))]
    public Task<FilteredInvoiceDto> GetFilteredInvoicesAsync
        (string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return invoiceManager.GetFilteredInvoicesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Invoices),nameof(Roles.Read))]
    public async Task<IEnumerable<InvoiceDto>> GlobalSearch(string search,string? column)
    {
        return await invoiceManager.GlobalSearch(search,column);
    }


}