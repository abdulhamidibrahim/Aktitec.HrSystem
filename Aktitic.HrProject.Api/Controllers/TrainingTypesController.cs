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
public class TrainingTypesController(ITrainingTypeManager trainingTypeManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Read))]
    public Task<List<TrainingTypeReadDto>> GetAll()
    {
        return trainingTypeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Read))]
    public ActionResult<TrainingTypeReadDto?> Get(int id)
    {
        var result = trainingTypeManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Add))]
    public ActionResult<Task> Add(TrainingTypeAddDto trainingTypeAddDto)
    {
        var result = trainingTypeManager.Add(trainingTypeAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Edit))]
    public ActionResult<Task> Update(TrainingTypeUpdateDto trainingTypeUpdateDto,int id)
    {
        var result= trainingTypeManager.Update(trainingTypeUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= trainingTypeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredTrainingTypes")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Read))]
    public Task<FilteredTrainingTypeDto> GetFilteredTrainingTypesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return trainingTypeManager.GetFilteredTrainingTypesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.TrainingType), nameof(Roles.Read))]
    public async Task<IEnumerable<TrainingTypeDto>> GlobalSearch(string search,string? column)
    {
        return await trainingTypeManager.GlobalSearch(search,column);
    }


}