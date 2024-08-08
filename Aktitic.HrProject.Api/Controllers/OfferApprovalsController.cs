using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfferApprovalsController(IOfferApprovalsManager offerApprovalsManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OfferApprovalReadDto>>> GetAll()
    {
        var offerApprovals = await offerApprovalsManager.GetAll();
        return Ok(offerApprovals);
    }
    
    [HttpGet("{id}")]
    public ActionResult<OfferApprovalReadDto?> Get(int id)
    {
        var offerApprovals = offerApprovalsManager.Get(id);
        if (offerApprovals == null) return NotFound("OfferApprovals not found");
        return Ok(offerApprovals);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] OfferApprovalAddDto offerApprovalsAddDto)
    {
        var result =offerApprovalsManager.Add(offerApprovalsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("OfferApprovals added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] OfferApprovalUpdateDto offerApprovalsUpdateDto,int id)
    {
        var result =offerApprovalsManager.Update(offerApprovalsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("OfferApprovals updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =offerApprovalsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<OfferApprovalDto>> GlobalSearch(string search,string? column)
    {
        return await offerApprovalsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredOfferApprovals")]
    public Task<FilteredOfferApprovalsDto> GetFilteredOfferApprovalsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return offerApprovalsManager.GetFilteredOfferApprovalsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}