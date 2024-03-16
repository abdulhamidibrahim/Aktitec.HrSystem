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
    private readonly IFileRepo _fileRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    // private readonly UserManager<Project> _userManager;

    public ProjectsController(
        IProjectManager projectManager, 
        IFileRepo fileRepo, 
        IWebHostEnvironment webHostEnvironment)
    {
        _projectManager = projectManager;
        _fileRepo = fileRepo;
        _webHostEnvironment = webHostEnvironment;
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
        Task<ProjectReadDto?> user = _projectManager.Get(id);

        return user;
    }
    
    
    [HttpGet("getFilteredProjects")]
    public Task<FilteredProjectDto> GetFilteredProjectsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _projectManager.GetFilteredProjectsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    // [ValidateAntiForgeryToken]
    [Consumes("multipart/form-data")]
    // [ProjectEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromForm] ProjectAddDto projectAddDto)
    {
        
            
            int result = await _projectManager.Add(projectAddDto);
            if (result.Equals(0))
            {
                return BadRequest("Account Creation Failed");
            }
            
            return Ok("Account Created Successfully ");
       
    }
    
    [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] ProjectUpdateDto projectUpdateDto,int id)
    {
        var result = _projectManager.Update(projectUpdateDto,id);
        if (result.Result.Equals(0))
        {
            return BadRequest("Account Update Failed");
        }
        return Ok("Account updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =_projectManager.Delete(id);
        if (result.Result.Equals(0))
        {
            return BadRequest("Account Deletion Failed");
        }
        return Ok();
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ProjectDto>> GlobalSearch(string search,string? column)
    {
        return await _projectManager.GlobalSearch(search,column);
    }
    
    
    
}