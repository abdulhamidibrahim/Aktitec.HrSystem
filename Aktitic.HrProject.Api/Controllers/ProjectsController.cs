using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using FileUploadingWebAPI.Filter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController: ControllerBase
{
    private readonly IProjectManager _projectManager;
    // private readonly UserManager<Project> _userManager;

    public ProjectsController(
        IProjectManager projectManager)
    {
        _projectManager = projectManager;
        // _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<List<ProjectReadDto>> GetAll()
    {
        return await _projectManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<ProjectReadDto?> Get(int id)
    {
        Task<ProjectReadDto?> result = _projectManager.Get(id);

        return result;
    }
    
    // [ValidateAntiForgeryToken]
    // [Consumes("multipart/form-data")]
    // [ProjectEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    public  ActionResult Create(ProjectAddDto projectAddDto)
    {
         var result =_projectManager.Add(projectAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok(" Created Successfully ");
    }
    
    // [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    public ActionResult Update( ProjectUpdateDto projectUpdateDto,int id)
    {
        var result = _projectManager.Update(projectUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok(" updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =_projectManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ProjectDto>> GlobalSearch(string search,string? column)
    {
        return await _projectManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredProjects")]
    public OkObjectResult GetFilteredProjectsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        try
        {
            var project =  _projectManager.GetFilteredProjectsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
            return Ok(project);  
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
    }
    
    
    
}