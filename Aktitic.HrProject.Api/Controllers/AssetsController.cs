using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController(IAssetsManager assetsManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Read))]
    public async Task<ActionResult<List<AssetsReadDto>>> GetAll()
    {
        var assets = await assetsManager.GetAll();
        return Ok(assets);
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Read))]
    public async Task<ActionResult<AssetsReadDto?>> Get(int id)
    {
        var assets =await assetsManager.Get(id);
        if (assets == null) return NotFound("Assets not found");
        return Ok(assets);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Add))]
    public ActionResult Add([FromBody] AssetsAddDto assetsAddDto)
    {
        var result =assetsManager.Add(assetsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Assets added successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Edit))]
    public ActionResult Update([FromBody] AssetsUpdateDto assetsUpdateDto,int id)
    {
        var result =assetsManager.Update(assetsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Assets updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    { 
        var result =assetsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Read))]
    public async Task<IEnumerable<AssetsDto>> GlobalSearch(string search,string? column)
    {
        return await assetsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredAssets")]
    [AuthorizeRole(nameof(Pages.Assets),nameof(Roles.Read))]
    public Task<FilteredAssetsDto> GetFilteredAssetsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return assetsManager.GetFilteredAssetsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}