using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeManager _employeeManager;
    private readonly IFileRepo _fileRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmployeesController(IEmployeeManager employeeManager, IFileRepo fileRepo, IWebHostEnvironment webHostEnvironment)
    {
        _employeeManager = employeeManager;
        _fileRepo = fileRepo;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [HttpGet]
    public async Task<List<EmployeeReadDto>> GetAll()
    {
        return await _employeeManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<EmployeeReadDto?> Get(int id)
    {
        var user = _employeeManager.Get(id);
        if (user == null) return Task.FromResult<EmployeeReadDto?>(null);
        var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        user.ImgUrl = hostUrl + user.ImgUrl; 
        return Task.FromResult(user)!;
    }
    
    [HttpPost("create")]
    public ActionResult Create(EmployeeAddDto employeeAddDto)
    {
        _employeeManager.Add(employeeAddDto);
        return Ok();
    }
    
    [HttpPut]
    public ActionResult Update(EmployeeUpdateDto employeeUpdateDto)
    {
        _employeeManager.Update(employeeUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(EmployeeDeleteDto employeeDeleteDto)
    {
        _employeeManager.Delete(employeeDeleteDto);
        return Ok();
    }
    
    // [HttpPost("uploadImage")]
    // [ImageValidator]
    // public IActionResult UploadImage(IFormFile image)
    // {
    //     var file = new File()
    //     {
    //         Name = image.Name,
    //         Extension = image.ContentType,
    //     };
    //     
    //     var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages",file.EmployeeId.ToString(), image.Name);
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
    //     fileModel.Result.Name = file.FileName;
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
    //     if (uploadedFile.Result is { Name: not null }) return Ok(Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages", uploadedFile.Result.Name));
    //     return BadRequest("Image Not Found");
    // }
    //
    // [HttpDelete("getImage/{id}")]
    // public IActionResult GetImage(int id)
    // {
    //     var uploadedFile = _fileRepo.GetById(id);
    //
    //     var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employeeImages", uploadedFile.Result.Name);
    //
    //     MemoryStream memoryStream = new();
    //     using FileStream fileStream = new(path, FileMode.Open);
    //     fileStream.CopyTo(memoryStream);
    //
    //     memoryStream.Position = 0;
    //
    //     if (uploadedFile.Result.Extension != null)
    //         return File(memoryStream, uploadedFile.Result.Extension, uploadedFile.Result.Name);
    //     return BadRequest("Image Not Found");
    // }
    
    
}