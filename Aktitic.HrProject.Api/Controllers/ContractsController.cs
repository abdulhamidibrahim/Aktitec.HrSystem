using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController: ControllerBase
{
    private readonly IContractsManager _contractManager;

    public ContractsController(IContractsManager contractManager)
    {
        _contractManager = contractManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ContractReadDto>>> GetAll()
    {
        return await _contractManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ContractReadDto?> Get(int id)
    {
        var result = _contractManager.Get(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    [HttpPost("create")]
    public ActionResult Add( ContractAddDto contractAddDto)
    {
         var result = _contractManager.Add(contractAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok("Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update( ContractUpdateDto contractUpdateDto,int id)
    {
        var result  =_contractManager.Update(contractUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _contractManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ContractDto>> GlobalSearch(string search,string? column)
    {
        return await _contractManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredContracts")]
    public Task<FilteredContractDto> GetFilteredContractsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _contractManager.GetFilteredContractsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}