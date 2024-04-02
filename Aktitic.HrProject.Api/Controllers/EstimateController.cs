using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstimateController: ControllerBase
{
    private readonly IEstimateManager _estimateManager;

    public EstimateController(IEstimateManager estimateManager)
    {
        _estimateManager = estimateManager;
    }
    
    [HttpGet]
    public Task<List<EstimateReadDto>> GetAll()
    {
        return _estimateManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<EstimateReadDto?> Get(int id)
    {
        var user = _estimateManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(EstimateAddDto estimateAddDto)
    {
        var result = _estimateManager.Add(estimateAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(EstimateUpdateDto estimateUpdateDto,int id)
    {
        var result= _estimateManager.Update(estimateUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _estimateManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredEstimates")]
    public Task<FilteredEstimateDto> GetFilteredEstimatesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _estimateManager.GetFilteredEstimatesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<EstimateDto>> GlobalSearch(string search,string? column)
    {
        return await _estimateManager.GlobalSearch(search,column);
    }


}