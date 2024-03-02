using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeManager _employeeManager;
    private readonly IFileRepo _fileRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<Employee> _userManager;

    public EmployeesController(
        IEmployeeManager employeeManager, 
        IFileRepo fileRepo, 
        IWebHostEnvironment webHostEnvironment,
        UserManager<Employee> userManager)
    {
        _employeeManager = employeeManager;
        _fileRepo = fileRepo;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
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
    
    [HttpGet("/getEmployees")]
    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string? term, string? sort, int page, int limit)
    {
        return await _employeeManager.GetEmployeesAsync(term, sort, page, limit);
    }
    
    [HttpGet("/getFilteredEmployees")]
    public IEnumerable<EmployeeDto> GetFilteredEmployeesAsync(string column, string value1,[Optional] string value2, string @operator)
    {
        return _employeeManager.GetFilteredEmployeesAsync(column, value1, value2, @operator);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Create(EmployeeAddDto employeeAddDto)
    {
        if (ModelState.IsValid)
        {
            var employee = new Employee()
            {
                FullName = employeeAddDto.FullName,
                Email = employeeAddDto.Email,
                UserName = employeeAddDto.Email!.Substring(0, employeeAddDto.Email.IndexOf('@')),
                // ImgUrl = employeeAddDto.ImgUrl,
                Phone = employeeAddDto.Phone,
                Age = employeeAddDto.Age,
                JobPosition = employeeAddDto.JobPosition,
                JoiningDate = employeeAddDto.JoiningDate,
                YearsOfExperience = employeeAddDto.YearsOfExperience,
                Salary = employeeAddDto.Salary,
                FileName = employeeAddDto.Image.FileName,
                FileContent = employeeAddDto.Image.FileName,
                FileExtension = employeeAddDto.Image.ContentType,
                // DepartmentId = employeeAddDto.DepartmentId,
                // ManagerId = employeeAddDto.ManagerId,
            };
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",employee.FullName!);
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }else
            {
                Directory.CreateDirectory(path);
            }
            employee.ImgUrl = path+"/"+ employee.FileName+".png";
            await using FileStream fileStream = new(employee.ImgUrl, FileMode.Create);
            // employeeAddDto.Image.CopyToAsync(fileStream);
            await employeeAddDto.Image.CopyToAsync(fileStream);
            
            IdentityResult result = await _userManager.CreateAsync(employee, employeeAddDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("error while creating user");
            }
            
            return Ok("Account Created Successfully ");
        }
        
        var errors = ModelState.Where (n => n.Value?.Errors.Count > 0).ToList ();
        return BadRequest(errors);
    }
    
    [HttpPut]
    public ActionResult Update(EmployeeUpdateDto employeeUpdateDto)
    {
        _employeeManager.Update(employeeUpdateDto);
        return Ok();
    }
    
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        _employeeManager.Delete(id);
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