
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class EmployeeManager:IEmployeeManager
{
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileRepo _fileRepo;
    private readonly UserManager<Employee> _userManager;
    private readonly IMapper _mapper;

    public EmployeeManager(IEmployeeRepo employeeRepo, IWebHostEnvironment webHostEnvironment, IFileRepo fileRepo, UserManager<Employee> userManager, IMapper mapper)
    {
        _employeeRepo = employeeRepo;
        _webHostEnvironment = webHostEnvironment;
        _fileRepo = fileRepo;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public void Add(EmployeeAddDto employeeAddDto)
    {
        var employee = new Employee()
        {
            FullName = employeeAddDto.FullName!,
            // Phone = employeeAddDto.Phone,
            Email = employeeAddDto.Email,
            // DepartmentId = employeeAddDto.DepartmentId,
            // JoiningDate = employeeAddDto.JoiningDate,
            // Salary = employeeAddDto.Salary,
            // JobPosition = employeeAddDto.JobPosition,
            // YearsOfExperience = employeeAddDto.YearsOfExperience,
            // ManagerId = employeeAddDto.ManagerId,
            Age = employeeAddDto.Age,
            // FileName = employeeAddDto.Image.FileName,
            // FileExtension = employeeAddDto.Image.ContentType,
        };
        // var file = new UserFile()
        // {
        //     Name = employeeAddDto.Image.FileName,
        //     // Content = image.OpenReadStream().ReadAllBytes(),
        //     Extension = employeeAddDto.Image.ContentType,
        //     
        // };
       
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",employee.FullName!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }else
        {
            Directory.CreateDirectory(path);
        }
        employee.ImgUrl = path+"/"+ employee.FileName+".png";
        using FileStream fileStream = new(employee.ImgUrl, FileMode.Create);
        // employeeAddDto.Image.CopyToAsync(fileStream);
    
        // _fileRepo.Add(file);
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
        // employee.Result.FileContent = employeeUpdateDto.Image.ContentType;
        employee.Result.FileExtension= employeeUpdateDto.Image.ContentType;
        employee.Result.FileName=employeeUpdateDto.Image.FileName;
        
        // var file = new UserFile()
        // {
        //     Name = EmployeeUpdateDto.Image.FileName,
        //     // Content = image.OpenReadStream().ReadAllBytes(),
        //     Extension = EmployeeUpdateDto.Image.ContentType,
        //     // EmployeeName = EmployeeUpdateDto.FullName
        // };
       
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",employee.Result.FullName!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }else
        {
            Directory.CreateDirectory(path);
        }
        employee.Result.ImgUrl = path+"/"+ employee.Result.FileName+".png";
        using FileStream fileStream = new(employee.Result.ImgUrl, FileMode.Create);
        employeeUpdateDto.Image.CopyToAsync(fileStream);

        
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

    public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string? term, string? sort, int page, int limit)
    {
        var employees = _employeeRepo.GetEmployeesAsync(term, sort, page, limit);
        return Task.FromResult(employees.Result.Employees.Select(employee => new EmployeeDto()
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Email = employee.Email,
            ImgUrl = employee.ImgUrl,
            ImgId = employee.ImgId,

        }));
    }

   

    public IEnumerable<EmployeeDto> GetFilteredEmployeesAsync(string column, string value1, string? value2,string @operator)
    {
        var users = _userManager.Users.ToList().AsQueryable();
        
        var employeeDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(users);
        IEnumerable<Employee> employees;
            // filtering logic implementation 
            @operator = @operator.ToLower();
            decimal employeeValue;
            switch (@operator)
            {
                case "contains":
                    employees = users.Where(e => 
                        e.GetValue(column).Contains(value1) || e.GetValue(column).Contains(value2));
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
                case "doesnotcontain":
                     employees= users.SkipWhile(e =>
                        e.GetValue(column).Contains(value1) || e.GetValue(column).Contains(value2));
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
                case "startswith":
                    employees = users.Where(e=> 
                        e.GetValue(column).StartsWith(value1) || e.GetValue(column).StartsWith(value2));
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
                case "endswith":
                    employees = users.Where(e=> 
                        e.GetValue(column).EndsWith(value1) || e.GetValue(column).EndsWith(value2));
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
                case "eq":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out employeeValue) &&
                        (employeeValue == decimal.Parse(value1) || employeeValue == decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                case "neq":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out employeeValue) &&
                        (employeeValue != decimal.Parse(value1) && employeeValue != decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                case "gte":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out employeeValue) &&
                        (employeeValue >= decimal.Parse(value1) || employeeValue >= decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                case "gt":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out  employeeValue) &&
                        (employeeValue > decimal.Parse(value1) || employeeValue > decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                case "lte":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out  employeeValue) &&
                        (employeeValue <= decimal.Parse(value1) || employeeValue <= decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

                case "lt":
                    employees = users.Where(e =>
                        decimal.TryParse(e.GetValue(column), out  employeeValue) &&
                        (employeeValue < decimal.Parse(value1) || employeeValue < decimal.Parse(value2))
                    );
                    return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);default:
                    return employeeDto;
            }
        }
    

    // public string? GetFilePath(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.Result.EmployeeName!, file.Result.Name);
    // }
}
