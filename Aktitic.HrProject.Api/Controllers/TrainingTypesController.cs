using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingTypesController: ControllerBase
{
    private readonly ITrainingTypeManager _trainingTypeManager;

    public TrainingTypesController(ITrainingTypeManager trainingTypeManager)
    {
        _trainingTypeManager = trainingTypeManager;
    }
    
    [HttpGet]
    public Task<List<TrainingTypeReadDto>> GetAll()
    {
        return _trainingTypeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TrainingTypeReadDto?> Get(int id)
    {
        var result = _trainingTypeManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TrainingTypeAddDto trainingTypeAddDto)
    {
        var result = _trainingTypeManager.Add(trainingTypeAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(TrainingTypeUpdateDto trainingTypeUpdateDto,int id)
    {
        var result= _trainingTypeManager.Update(trainingTypeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _trainingTypeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainingTypes")]
    public Task<FilteredTrainingTypeDto> GetFilteredTrainingTypesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _trainingTypeManager.GetFilteredTrainingTypesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TrainingTypeDto>> GlobalSearch(string search,string? column)
    {
        return await _trainingTypeManager.GlobalSearch(search,column);
    }


}