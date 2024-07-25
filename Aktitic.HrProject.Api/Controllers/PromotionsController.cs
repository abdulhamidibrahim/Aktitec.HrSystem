using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionsController: ControllerBase
{
    private readonly IPromotionManager _promotionsManager;

    public PromotionsController(IPromotionManager promotionManager)
    {
        _promotionsManager = promotionManager;
    }
    
    [HttpGet]
    public Task<List<PromotionReadDto>> GetAll()
    {
        return _promotionsManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<PromotionReadDto?> Get(int id)
    {
        var result = _promotionsManager.Get(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add( PromotionAddDto promotionsAddDto)
    {
        var result = _promotionsManager.Add(promotionsAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update( PromotionUpdateDto promotionsUpdateDto,int id)
    {
        var result= _promotionsManager.Update(promotionsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _promotionsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredPromotions")]
    public Task<FilteredPromotionsDto> GetFilteredPromotionsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _promotionsManager.GetFilteredPromotionAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<PromotionDto>> GlobalSearch(string search,string? column)
    {
        return await _promotionsManager.GlobalSearch(search,column);
    }


}