using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using FileUploadingWebAPI.Filter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController: ControllerBase
{
    private readonly IClientManager _clientManager;
    private readonly IFileRepo _fileRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    // private readonly UserManager<Client> _userManager;

    public ClientsController(
        IClientManager clientManager, 
        IFileRepo fileRepo, 
        IWebHostEnvironment webHostEnvironment)
    {
        _clientManager = clientManager;
        _fileRepo = fileRepo;
        _webHostEnvironment = webHostEnvironment;
        // _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<List<ClientReadDto>> GetAll()
    {
        return await _clientManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<ClientReadDto?> Get(int id)
    {
        var user = _clientManager.Get(id);
        if (user == null) return Task.FromResult<ClientReadDto?>(null);
        var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        user.ImgUrl = hostUrl + user.ImgUrl; 
        return Task.FromResult(user)!;
    }
    
    [HttpGet("getClients")]
    public async Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit)
    {
        return await _clientManager.GetClientsAsync(term, sort, page, limit);
    }
    
    [HttpGet("getFilteredClients")]
    public Task<FilteredClientDto> GetFilteredClientsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _clientManager.GetFilteredClientsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    // [ValidateAntiForgeryToken]
    [Consumes("multipart/form-data")]
    // [ClientEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromForm] ClientAddDto clientAddDto,[FromForm] IFormFile? image)
    {
        
            
            int result = await _clientManager.Add(clientAddDto,image);
            if (result.Equals(0))
            {
                return BadRequest("Account Creation Failed");
            }
            
            return Ok("Account Created Successfully ");
        // }
        
        // var errors = ModelState.Where (n => n.Value?.Errors.Count > 0).ToList ();
        // return BadRequest(errors);
    }
    
    [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] ClientUpdateDto clientUpdateDto,int id,[FromForm] IFormFile? image)
    {
        var result = _clientManager.Update(clientUpdateDto,id,image);
        if (result.Result.Equals(0))
        {
            return BadRequest("Account Update Failed");
        }
        return Ok("Account updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =_clientManager.Delete(id);
        if (result.Result.Equals(0))
        {
            return BadRequest("Account Deletion Failed");
        }
        return Ok();
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ClientDto>> GlobalSearch(string search,string? column)
    {
        return await _clientManager.GlobalSearch(search,column);
    }
    
    
}