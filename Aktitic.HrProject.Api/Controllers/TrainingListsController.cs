using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingListsController: ControllerBase
{
    private readonly ITrainingListManager _trainingListManager;

    public TrainingListsController(ITrainingListManager trainingListManager)
    {
        _trainingListManager = trainingListManager;
    }
    
    [HttpGet]
    public Task<List<TrainingListReadDto>> GetAll()
    {
        return _trainingListManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TrainingListReadDto?> Get(int id)
    {
        var result = _trainingListManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TrainingListAddDto trainingListAddDto)
    {
        var result = _trainingListManager.Add(trainingListAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(TrainingListUpdateDto trainingListUpdateDto,int id)
    {
        var result= _trainingListManager.Update(trainingListUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _trainingListManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainingLists")]
    public Task<FilteredTrainingListDto> GetFilteredTrainingListsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _trainingListManager.GetFilteredTrainingListsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TrainingListDto>> GlobalSearch(string search,string? column)
    {
        return await _trainingListManager.GlobalSearch(search,column);
    }


}