using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Managers.Company;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
// [Authorize(Role = "SystemOwner")] 
[Route("api/[controller]")]
public class CompaniesController(ICompanyManager companyManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyReadDto>>> GetAll()
    {
        var result = await companyManager.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<CompanyReadDto?> Get(int id)
    {
        var result = companyManager.Get(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    [HttpPost("create")]
    public ActionResult Add( CompanyAddDto companyAddDto)
    {
         var result = companyManager.Add(companyAddDto);
         if (result.Result == 0) return BadRequest("Failed to add");
         return Ok(new {message ="Created Successfully",CompanyId = result.Result});
    }
    

    [HttpPost("createAdmin")]
    public async Task<ActionResult> AddAdmin(CompanyAddDto companyAddDto)
    {
        var result = await companyManager.AddAdmin(companyAddDto);
        if (result == 0) return BadRequest("Failed to add");
        return Ok(new { message = "Created Successfully", CompanyId = result });
    }

    [HttpPut("update/{id}")]
    public ActionResult Update( CompanyUpdateDto companyUpdateDto,int id)
    {
        var result  =companyManager.Update(companyUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpPut("updateCompany/{id}")]
    public ActionResult UpdateCompany( CompanyDto companyUpdateDto,int id)
    {
        var result  =companyManager.UpdateCompany(companyUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = companyManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<CompanyReadDto>> GlobalSearch(string search,string? column)
    {
        return await companyManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredCompanies")]
    public Task<FilteredCompanyDto> GetFilteredCompaniesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return companyManager.GetFilteredCompaniesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
  
    
    [HttpPost("UploadLogo")]
    public ActionResult UploadLogo( IFormFile file,int companyId)
    {
        var result =companyManager.UploadLogo(file,companyId);
        if (result.Result == 0) return BadRequest("Failed to save logo");
        return Ok("Uploaded Successfully");
    }
}