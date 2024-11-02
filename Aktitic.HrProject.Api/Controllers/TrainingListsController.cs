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
public class TrainingListsController(ITrainingListManager trainingListManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Read))]
    public Task<List<TrainingListReadDto>> GetAll()
    {
        return trainingListManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Read))]
    public ActionResult<TrainingListReadDto?> Get(int id)
    {
        var result = trainingListManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Add))]
    public ActionResult<Task> Add(TrainingListAddDto trainingListAddDto)
    {
        var result = trainingListManager.Add(trainingListAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Edit))]
    public ActionResult<Task> Update(TrainingListUpdateDto trainingListUpdateDto,int id)
    {
        var result= trainingListManager.Update(trainingListUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= trainingListManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainingLists")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Read))]
    public Task<FilteredTrainingListDto> GetFilteredTrainingListsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return trainingListManager.GetFilteredTrainingListsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.TrainingList), nameof(Roles.Read))]
    public async Task<IEnumerable<TrainingListDto>> GlobalSearch(string search,string? column)
    {
        return await trainingListManager.GlobalSearch(search,column);
    }


}