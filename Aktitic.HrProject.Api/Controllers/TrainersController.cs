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
public class TrainersController(ITrainerManager trainerManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Read))]
    public Task<List<TrainerReadDto>> GetAll()
    {
        return trainerManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Read))]
    public ActionResult<TrainerReadDto?> Get(int id)
    {
        var result = trainerManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Add))]
    public ActionResult<Task> Add(TrainerAddDto trainerAddDto)
    {
        var result = trainerManager.Add(trainerAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Edit))]
    public ActionResult<Task> Update(TrainerUpdateDto trainerUpdateDto,int id)
    {
        var result= trainerManager.Update(trainerUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= trainerManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainers")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Read))]
    public Task<FilteredTrainerDto> GetFilteredTrainersAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return trainerManager.GetFilteredTrainersAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Trainers), nameof(Roles.Read))]
    public async Task<IEnumerable<TrainerDto>> GlobalSearch(string search,string? column)
    {
        return await trainerManager.GlobalSearch(search,column);
    }


}