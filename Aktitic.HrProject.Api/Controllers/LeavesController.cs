using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeavesController: ControllerBase
{
    private readonly ILeavesManager _leaveManager;

    public LeavesController(ILeavesManager leaveManager)
    {
        _leaveManager = leaveManager;
    }
    
    [HttpGet]
    public ActionResult<List<LeavesReadDto>> GetAll()
    {
        return _leaveManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<LeavesReadDto?> Get(int id)
    {
        var user = _leaveManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost]
    public ActionResult Add(LeavesAddDto leaveAddDto)
    {
        _leaveManager.Add(leaveAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(LeavesUpdateDto leaveUpdateDto)
    {
        _leaveManager.Update(leaveUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(LeavesDeleteDto leaveDeleteDto)
    {
        _leaveManager.Delete(leaveDeleteDto);
        return Ok();
    }
    
}