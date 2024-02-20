using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HolidaysController: ControllerBase
{
    private readonly IHolidayManager _holidayManager;

    public HolidaysController(IHolidayManager holidayManager)
    {
        _holidayManager = holidayManager;
    }
    
    [HttpGet]
    public ActionResult<List<HolidayReadDto>> GetAll()
    {
        return _holidayManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<HolidayReadDto?> Get(int id)
    {
        var user = _holidayManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost]
    public ActionResult Add(HolidayAddDto holidayAddDto)
    {
        _holidayManager.Add(holidayAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(HolidayUpdateDto holidayUpdateDto)
    {
        _holidayManager.Update(holidayUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(HolidayDeleteDto holidayDeleteDto)
    {
        _holidayManager.Delete(holidayDeleteDto);
        return Ok();
    }
    
}