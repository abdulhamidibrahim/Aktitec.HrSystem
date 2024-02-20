using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesignationsController: ControllerBase
{
    private readonly IDesignationManager _designationManager;

    public DesignationsController(IDesignationManager designationManager)
    {
        _designationManager = designationManager;
    }
    
    [HttpGet]
    public ActionResult<List<DesignationReadDto>> GetAll()
    {
        return _designationManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<DesignationReadDto?> Get(int id)
    {
        var user = _designationManager.Get(id);
        if (user == null) return NotFound();
        return user;
    }
    
    [HttpPost]
    public ActionResult Add(DesignationAddDto designationAddDto)
    {
        _designationManager.Add(designationAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(DesignationUpdateDto designationUpdateDto)
    {
        _designationManager.Update(designationUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(DesignationDeleteDto designationDeleteDto)
    {
        _designationManager.Delete(designationDeleteDto);
        return Ok();
    }
    
}