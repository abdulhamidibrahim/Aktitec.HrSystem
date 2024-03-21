using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomPolicyController: ControllerBase
{
    private readonly ICustomPolicyManager _customPolicyManager;

    public CustomPolicyController(ICustomPolicyManager customPolicyManager)
    {
        _customPolicyManager = customPolicyManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CustomPolicyReadDto>>> GetAll()
    {
        var result = await _customPolicyManager.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public  ActionResult<CustomPolicyReadDto?> Get(int id)
    {
        var user = _customPolicyManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user.Result);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromBody] CustomPolicyAddDto customPolicyAddDto)
    {
        var result = _customPolicyManager.Add(customPolicyAddDto);
        if (result.Result.Equals(0)) return BadRequest("Failed to create");
        return Ok("Created successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] CustomPolicyUpdateDto customPolicyUpdateDto,int id)
    {
        _customPolicyManager.Update(customPolicyUpdateDto,id);
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _customPolicyManager.Delete(id);
        return Ok("Deleted successfully");
    }
    
}