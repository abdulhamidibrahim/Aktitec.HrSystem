using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using FileUploadingWebAPI.Filter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Aktitic.HrProject.DAL.Models.Pages;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectManager projectManager) : ControllerBase
{
    // private readonly UserManager<Project> _userManager;

    // _userManager = userManager;

    [HttpGet]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Read))]
    
    public async Task<List<ProjectReadDto>> GetAll()
    {
        return await projectManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Read))]
    public Task<ProjectReadDto?> Get(int id)
    {
        Task<ProjectReadDto?> result = projectManager.Get(id);

        return result;
    }
    
    // [ValidateAntiForgeryToken]
    // [Consumes("multipart/form-data")]
    // [ProjectEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Add))]
    public  ActionResult Create(ProjectAddDto projectAddDto)
    {
         var result =projectManager.Add(projectAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok(" Created Successfully ");
    }
    
    // [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Edit))]
    public ActionResult Update( ProjectUpdateDto projectUpdateDto,int id)
    {
        var result = projectManager.Update(projectUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok(" updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    {
        var result =projectManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully");
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Read))]
    public async Task<IEnumerable<ProjectDto>> GlobalSearch(string search,string? column)
    {
        return await projectManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredProjects")]
    [AuthorizeRole(nameof(Pages.Projects), nameof(Roles.Read))]
    public OkObjectResult GetFilteredProjectsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        try
        {
            var project =  projectManager.GetFilteredProjectsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
            return Ok(project);  
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
    }
    
    
    
}