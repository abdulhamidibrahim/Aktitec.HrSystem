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
    // private readonly IWebHostEnvironment _webHostEnvironment;
    // private readonly UserManager<Client> _userManager;

    public ClientsController(
        IClientManager clientManager
        // IWebHostEnvironment webHostEnvironment
        )
    {
        _clientManager = clientManager;
        // _webHostEnvironment = webHostEnvironment;
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
        var result = _clientManager.Get(id);
        if (result == null) return Task.FromResult<ClientReadDto?>(null);
        var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        result.PhotoUrl = hostUrl + result.PhotoUrl; 
        return Task.FromResult(result)!;
    }
    
    // [HttpGet("getClients")]
    // public async Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit)
    // {
    //     return await _clientManager.GetClientsAsync(term, sort, page, limit);
    // }
    //
    [HttpGet("getFilteredClients")]
    public Task<FilteredClientDto> GetFilteredClientsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _clientManager.GetFilteredClientsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    // [ValidateAntiForgeryToken]
    // [Consumes("multipart/form-data")]
    // [ClientEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    public  ActionResult Create([FromForm] ClientAddDto clientAddDto)
    {
           var result = _clientManager.Add(clientAddDto);

           if (result.Result == 0) return BadRequest("Failed to create");
           return Ok("Created Successfully ");
       
    }
    
    // [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] ClientUpdateDto clientUpdateDto, int id)
    {
         var result = _clientManager.Update(clientUpdateDto,id);
        if (result.Result == Task.FromResult(0)) return BadRequest("Failed to update");
        return Ok("updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
           var result =  _clientManager.Delete(id);
           if (result.Result > (0))
                return Ok(" deleted successfully!");
           
           return BadRequest("Failed to delete");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ClientDto>> GlobalSearch(string search,string? column)
    {
        return await _clientManager.GlobalSearch(search,column);
    }
    
    
}