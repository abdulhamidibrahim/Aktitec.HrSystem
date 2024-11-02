using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewQuestionsController(IInterviewQuestionsManager interviewQuestionsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InterviewQuestionsReadDto>>> GetAll()
    {
        var assets = await interviewQuestionsManager.GetAll();
        return Ok(assets);
    }
    
    [HttpGet("{id}")]
    public ActionResult<InterviewQuestionsReadDto?> Get(int id)
    {
        var assets = interviewQuestionsManager.Get(id);
        if (assets == null) return NotFound("InterviewQuestions not found");
        return Ok(assets);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] InterviewQuestionsAddDto assetsAddDto)
    {
        var result =interviewQuestionsManager.Add(assetsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("InterviewQuestions added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] InterviewQuestionsUpdateDto assetsUpdateDto,int id)
    {
        var result =interviewQuestionsManager.Update(assetsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("InterviewQuestions updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =interviewQuestionsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<InterviewQuestionsDto>> GlobalSearch(string search,string? column)
    {
        return await interviewQuestionsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredInterviewQuestions")]
    public Task<FilteredInterviewQuestionsDto> GetFilteredInterviewQuestionsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return interviewQuestionsManager.GetFilteredInterviewQuestionsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}