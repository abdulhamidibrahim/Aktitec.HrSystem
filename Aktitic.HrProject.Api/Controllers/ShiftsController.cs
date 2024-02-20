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
    
    [HttpPost]
    public ActionResult Add(ShiftAddDto shiftAddDto)
    {
        _shiftManager.Add(shiftAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(ShiftUpdateDto shiftUpdateDto)
    {
        _shiftManager.Update(shiftUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(ShiftDeleteDto shiftDeleteDto)
    {
        _shiftManager.Delete(shiftDeleteDto);
        return Ok();
    }
    
}