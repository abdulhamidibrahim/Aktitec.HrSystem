using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController: ControllerBase
{
    private readonly IShiftManager _shiftManager;

    public ShiftsController(IShiftManager shiftManager)
    {
        _shiftManager = shiftManager;
    }
    
    [HttpGet]
    public ActionResult<List<ShiftReadDto>> GetAll()
    {
        return _shiftManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ShiftReadDto?> Get(int id)
    {
        var user = _shiftManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] ShiftAddDto shiftAddDto)
    {
        _shiftManager.Add(shiftAddDto);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] ShiftUpdateDto shiftUpdateDto,int id)
    {
        _shiftManager.Update(shiftUpdateDto,id);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _shiftManager.Delete(id);
        return Ok();
    }
    
}