
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Microsoft.AspNetCore.Hosting;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class EmployeeManager:IEmployeeManager
{
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileRepo _fileRepo;

    public EmployeeManager(IEmployeeRepo employeeRepo, IWebHostEnvironment webHostEnvironment, IFileRepo fileRepo)
    {
        _employeeRepo = employeeRepo;
        _webHostEnvironment = webHostEnvironment;
        _fileRepo = fileRepo;
    }
    
    public void Add(EmployeeAddDto employeeAddDto)
    {
        var employee = new Employee()
        {
            FullName = employeeAddDto.FullName!,
            Phone = employeeAddDto.Phone,
            Email = employeeAddDto.Email,
            DepartmentId = employeeAddDto.DepartmentId,
            JoiningDate = employeeAddDto.JoiningDate,
            Salary = employeeAddDto.Salary,
            JobPosition = employeeAddDto.JobPosition,
            YearsOfExperience = employeeAddDto.YearsOfExperience,
            ManagerId = employeeAddDto.ManagerId,
            Age = employeeAddDto.Age,
            
        };
        var file = new File()
        {
            Name = employeeAddDto.Image.FileName,
            // Content = image.OpenReadStream().ReadAllBytes(),
            Extension = employeeAddDto.Image.ContentType,
            EmployeeName = employeeAddDto.FullName,
        };
       
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.EmployeeName!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }else
        {
            Directory.CreateDirectory(path);
        }
        employee.ImgUrl = path+"/"+ file.EmployeeName+".png";
        using FileStream fileStream = new(employee.ImgUrl, FileMode.Create);
        employeeAddDto.Image.CopyToAsync(fileStream);
    
        _fileRepo.Add(file);
        _employeeRepo.Add(employee);
    }
    

    public void Update(EmployeeUpdateDto employeeUpdateDto)
    {
        var employee = _employeeRepo.GetById(employeeUpdateDto.Id);
        
        if (employee.Result == null) return;
        employee.Result.FullName = employeeUpdateDto.FullName;
        employee.Result.Phone = employeeUpdateDto.Phone;
        employee.Result.Email = employeeUpdateDto.Email;
        employee.Result.DepartmentId = employeeUpdateDto.DepartmentId;
        employee.Result.JoiningDate = employeeUpdateDto.JoiningDate;
        employee.Result.Salary = employeeUpdateDto.Salary;
        employee.Result.JobPosition = employeeUpdateDto.JobPosition;
        employee.Result.YearsOfExperience = employeeUpdateDto.YearsOfExperience;
        employee.Result.ManagerId = employeeUpdateDto.ManagerId;
        employee.Result.Age = employeeUpdateDto.Age;
        
        var file = new File()
        {
            Name = EmployeeUpdateDto.Image.FileName,
            // Content = image.OpenReadStream().ReadAllBytes(),
            Extension = EmployeeUpdateDto.Image.ContentType,
            // EmployeeName = EmployeeUpdateDto.FullName
        };
       
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.EmployeeName!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }else
        {
            Directory.CreateDirectory(path);
        }
        employee.Result.ImgUrl = path+"/"+ file.EmployeeName+".png";
        using FileStream fileStream = new(employee.Result.ImgUrl, FileMode.Create);
        EmployeeUpdateDto.Image.CopyToAsync(fileStream);

        
        _employeeRepo.Update(employee.Result);
    }

    public void Delete(EmployeeDeleteDto employeeDeleteDto)
    {
        var employee = _employeeRepo.GetById(employeeDeleteDto.Id);
        if (employee.Result != null) _employeeRepo.Delete(employee.Result);
    }

    public EmployeeReadDto? Get(int id)
    {
        var employee = _employeeRepo.GetById(id);
        if (employee.Result == null) return null;
        return new EmployeeReadDto()
        {
            FullName = employee.Result.FullName,
            Phone = employee.Result.Phone,
            Email = employee.Result.Email,
            Age = employee.Result.Age,
            JobPosition = employee.Result.JobPosition,
            JoiningDate = employee.Result.JoiningDate,
            YearsOfExperience = employee.Result.YearsOfExperience,
            Salary = employee.Result.Salary,
            DepartmentId = employee.Result.DepartmentId,
            ManagerId = employee.Result.ManagerId,
            ImgUrl = employee.Result.ImgUrl,
        };
    }

    public Task<List<EmployeeReadDto>> GetAll()
    {
        var employees = _employeeRepo.GetAll();
        
        return  Task.FromResult(employees.Result.Select(employee => new EmployeeReadDto()
        {
            FullName = employee.FullName,
            Phone = employee.Phone,
            Email = employee.Email,
            Age = employee.Age,
            JobPosition = employee.JobPosition,
            JoiningDate = employee.JoiningDate,
            YearsOfExperience = employee.YearsOfExperience,
            Salary = employee.Salary,
            DepartmentId = employee.DepartmentId,
            ManagerId = employee.ManagerId,
            ImgUrl = employee.ImgUrl
            
        }).ToList());
    }
    
    // public string? GetFilePath(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.Result.EmployeeName!, file.Result.Name);
    // }
}
