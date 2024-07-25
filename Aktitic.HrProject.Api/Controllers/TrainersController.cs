using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController: ControllerBase
{
    private readonly ITrainerManager _trainerManager;

    public TrainersController(ITrainerManager trainerManager)
    {
        _trainerManager = trainerManager;
    }
    
    [HttpGet]
    public Task<List<TrainerReadDto>> GetAll()
    {
        return _trainerManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TrainerReadDto?> Get(int id)
    {
        var result = _trainerManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(TrainerAddDto trainerAddDto)
    {
        var result = _trainerManager.Add(trainerAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(TrainerUpdateDto trainerUpdateDto,int id)
    {
        var result= _trainerManager.Update(trainerUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _trainerManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainers")]
    public Task<FilteredTrainerDto> GetFilteredTrainersAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _trainerManager.GetFilteredTrainersAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<TrainerDto>> GlobalSearch(string search,string? column)
    {
        return await _trainerManager.GlobalSearch(search,column);
    }


}