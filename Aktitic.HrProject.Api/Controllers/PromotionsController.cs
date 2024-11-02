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
public class PromotionsController(IPromotionManager promotionManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Read))]
    public Task<List<PromotionReadDto>> GetAll()
    {
        return promotionManager.GetAll();
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Read))]
    public ActionResult<PromotionReadDto?> Get(int id)
    {
        var result = promotionManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Add))]
    public ActionResult<Task> Add( PromotionAddDto promotionsAddDto)
    {
        var result = promotionManager.Add(promotionsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Edit))]
    public ActionResult<Task> Update( PromotionUpdateDto promotionsUpdateDto,int id)
    {
        var result= promotionManager.Update(promotionsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Delete))]
    public ActionResult<Task> Delete(int id)
    {
        var result= promotionManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPromotions")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Read))]
    public Task<FilteredPromotionsDto> GetFilteredPromotionsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return promotionManager.GetFilteredPromotionAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Promotion), nameof(Roles.Read))]
    public async Task<IEnumerable<PromotionDto>> GlobalSearch(string search,string? column)
    {
        return await promotionManager.GlobalSearch(search,column);
    }


}