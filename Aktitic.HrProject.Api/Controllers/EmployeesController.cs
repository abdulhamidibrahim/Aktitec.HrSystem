using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using FileUploadingWebAPI.Filter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeManager _employeeManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    // private readonly UserManager<Employee> _userManager;

    public EmployeesController(
        IEmployeeManager employeeManager, 
        IWebHostEnvironment webHostEnvironment)
    {
        _employeeManager = employeeManager;
        _webHostEnvironment = webHostEnvironment;
        // _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<List<EmployeeReadDto>> GetAll()
    {
        return await _employeeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Task<EmployeeReadDto?>> Get(int id)
    {
        var result = _employeeManager.Get(id);
        if (result == null) return Task.FromResult<EmployeeReadDto?>(null);
        var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        result.ImgUrl = hostUrl + result.ImgUrl; 
        return Ok(result!);
    }
    
    [HttpGet("getEmployees")]
    public async Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit)
    {
        return await _employeeManager.GetEmployeesAsync(term, sort, page, limit);
    }
    
    [HttpGet("getFilteredEmployees")]
    public Task<FilteredEmployeeDto> GetFilteredEmployeesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _employeeManager.GetFilteredEmployeesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    // [ValidateAntiForgeryToken]
    // [Consumes("multipart/form-data")]//
    // [EmployeeEmailAddressValidator]
    // [DisableFormValueModelBinding]
    [HttpPost("create")]
    public  ActionResult Create([FromForm] EmployeeAddDto employeeAddDto, IFormFile? image)
    {
        
        var result =_employeeManager.Add(employeeAddDto,image);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Account Created Successfully ");
        // }
        
        // var errors = ModelState.Where (n => n.Value?.Errors.Count > 0).ToList ();
        // return BadRequest(errors);
    }
    
    // [Consumes("multipart/form-data")]
    // [DisableFormValueModelBinding]
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] EmployeeUpdateDto employeeUpdateDto,int id, IFormFile? image)
    {
        var result= _employeeManager.Update(employeeUpdateDto,id,image);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Account updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result =_employeeManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("deleted successfully");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<EmployeeDto>> GlobalSearch(string search,string? column)
    {
        return await _employeeManager.GlobalSearch(search,column);
    }

    [HttpGet("getManagerTree")]
    public async Task<List<ManagerTree>?> GetManagerTree()
    {
        return await _employeeManager.GetManagersTreeAsync();
    }

    #region commented

    
    // [HttpPost("uploadImage")]
    // [ImageValidator]
    // public IActionResult UploadImage(IFormFile image)
    // {
    //     var file = new File()
    //     {
    //         FileName = image.FileName,
    //         Extension = image.ContentType,
    //     };
    //     
    //     var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages",file.EmployeeId.ToString(), image.FileName);
    //
    //     using FileStream fileStream = new(path, FileMode.Create);
    //     fileStream.CopyToAsync(fileStream);
    //
    //     _fileRepo.Add(file);
    //     return Ok("Image Uploaded Successfully");
    // }
    //
    //
    // [HttpPut("{id}")]
    // public IActionResult UpdateImage(int id, IFormFile file)
    // {
    //     var fileModel = _fileRepo.GetById(id);
    //     if (fileModel.Result == null) return NotFound();
    //     fileModel.Result.FileName = file.FileName;
    //     // using (var ms = new MemoryStream()) 
    //     // {
    //     //     file.CopyTo(ms);
    //     //     fileModel.Result.Content = ms.ToArray();
    //     // }
    //     
    //     var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages", file.FileName);
    //
    //     using FileStream fileStream = new(path, FileMode.Create);
    //     fileStream.CopyToAsync(fileStream);
    //
    //     
    //     _fileRepo.Update(fileModel.Result);
    //
    //     return Ok("Image Updated Successfully");
    // }
    //
    // [HttpGet("getImageUrl/{id}")]
    // public  IActionResult  GetImageUrl(int id)
    // {
    //     var uploadedFile = _fileRepo.GetById(id);
    //
    //     if (uploadedFile.Result is { FileName: not null }) return Ok(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages", uploadedFile.Result.FileName));
    //     return BadRequest("Image Not Found");
    // }
    //
    // [HttpDelete("getImage/{id}")]
    // public IActionResult GetImage(int id)
    // {
    //     var uploadedFile = _fileRepo.GetById(id);
    //
    //     var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages", uploadedFile.Result.FileName);
    //
    //     MemoryStream memoryStream = new();
    //     using FileStream fileStream = new(path, FileMode.Open);
    //     fileStream.CopyTo(memoryStream);
    //
    //     memoryStream.Position = 0;
    //
    //     if (uploadedFile.Result.Extension != null)
    //         return File(memoryStream, uploadedFile.Result.Extension, uploadedFile.Result.FileName);
    //     return BadRequest("Image Not Found");
    // }
    

    #endregion
    
}