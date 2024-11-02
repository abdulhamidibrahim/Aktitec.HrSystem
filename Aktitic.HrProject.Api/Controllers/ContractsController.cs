using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(
    IContractsManager contractManager ) : ControllerBase
{
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Read))]
    [HttpGet]
    public async Task<ActionResult<List<ContractReadDto>>> GetAll()
    {
        return await contractManager.GetAll();
    }
    
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Read))]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContractReadDto?>>? Get(int id)
    {

        var result = contractManager.Get(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Add))]
    public async Task<ActionResult> Add( ContractAddDto contractAddDto)
    {
      
        var result =await contractManager.Add(contractAddDto);
        if (result == 0) return BadRequest("Failed to add");
        return Ok("Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Edit))]
    public async Task<ActionResult> Update( ContractUpdateDto contractUpdateDto,int id)
    {
        
        var result  = await contractManager.Update(contractUpdateDto,id);
        if (result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Delete))]
    public async Task<ActionResult> Delete(int id)
    {

        var result =await contractManager.Delete(id);
        if (result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Read))]
    public async Task<ActionResult<IEnumerable<ContractDto>>> GlobalSearch(string search,string? column)
    {

        return await contractManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredContracts")]
    [AuthorizeRole(nameof(Pages.Contracts),nameof(Roles.Read))]
    public async Task<ActionResult<FilteredContractDto>> GetFilteredContractsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        return await contractManager.GetFilteredContractsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}