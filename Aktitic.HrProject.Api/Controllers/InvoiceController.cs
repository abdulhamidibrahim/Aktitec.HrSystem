using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController: ControllerBase
{
    private readonly IInvoiceManager _invoiceManager;

    public InvoiceController(IInvoiceManager invoiceManager)
    {
        _invoiceManager = invoiceManager;
    }
    
    [HttpGet]
    public Task<List<InvoiceReadDto>> GetAll()
    {
        return _invoiceManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<InvoiceReadDto?> Get(int id)
    {
        var user = _invoiceManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(InvoiceAddDto invoiceAddDto)
    {
        var result = _invoiceManager.Add(invoiceAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(InvoiceUpdateDto invoiceUpdateDto,int id)
    {
        var result= _invoiceManager.Update(invoiceUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _invoiceManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredInvoices")]
    public Task<FilteredInvoiceDto> GetFilteredInvoicesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _invoiceManager.GetFilteredInvoicesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<InvoiceDto>> GlobalSearch(string search,string? column)
    {
        return await _invoiceManager.GlobalSearch(search,column);
    }


}