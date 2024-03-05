
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.BL.Helpers;
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

public class EmployeeManager(
    IEmployeeRepo employeeRepo,
    IWebHostEnvironment webHostEnvironment,
    IFileRepo fileRepo,
    IMapper mapper)
    : IEmployeeManager
{
    private readonly IFileRepo _fileRepo = fileRepo;
    // private readonly UserManager<Employee> _userManager;

    // _userManager = userManager;

    public Task<int> Add(EmployeeAddDto employeeAddDto)
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
            Gender = employeeAddDto.Gender,
            ManagerId = employeeAddDto.ManagerId,
            Age = employeeAddDto.Age,
            FileName = employeeAddDto.Image?.FileName,
            FileExtension = employeeAddDto.Image?.ContentType,
            UserName = employeeAddDto.Email?.Substring(0,employeeAddDto.Email.IndexOf('@'))
        };

        #region commented

        // var file = new UserFile()
        // {
        //     Name = employeeAddDto.Image.FileName,
        //     // Content = image.OpenReadStream().ReadAllBytes(),
        //     Extension = employeeAddDto.Image.ContentType,
        //     
        // };
        #endregion
        
        var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/employees",employee.UserName!);
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }else
        {
            Directory.CreateDirectory(path);
        }
        employee.ImgUrl = path+"/"+ employee.FileName+".png";
        using FileStream fileStream = new(employee.ImgUrl, FileMode.Create);
        employeeAddDto.Image?.CopyToAsync(fileStream);
    
        // _fileRepo.Add(file);
        

       
        
        return employeeRepo.Add(employee);
    }
    

    public async Task<int> Update(EmployeeUpdateDto employeeUpdateDto,int id)
    {
        var employee = employeeRepo.GetById(id);
        
        if (employee.Result == null) return 0;
        if (employeeUpdateDto.FullName != null) employee.Result.FullName = employeeUpdateDto.FullName;
        if (employeeUpdateDto.Phone != null) employee.Result.Phone = employeeUpdateDto.Phone;
        if (employeeUpdateDto.Email != null) employee.Result.Email = employeeUpdateDto.Email;
        if (employeeUpdateDto.DepartmentId != null) employee.Result.DepartmentId = employeeUpdateDto.DepartmentId;
        if (employeeUpdateDto.JoiningDate != null) employee.Result.JoiningDate = employeeUpdateDto.JoiningDate;
        if (employeeUpdateDto.Salary != null) employee.Result.Salary = employeeUpdateDto.Salary;
        if (employeeUpdateDto.JobPosition != null) employee.Result.JobPosition = employeeUpdateDto.JobPosition;
        if (employeeUpdateDto.YearsOfExperience != null) employee.Result.YearsOfExperience = employeeUpdateDto.YearsOfExperience;
        if (employeeUpdateDto.ManagerId != null) employee.Result.ManagerId = employeeUpdateDto.ManagerId;
        if (employeeUpdateDto.Gender != null) employee.Result.Gender = employeeUpdateDto.Gender;
        if (employeeUpdateDto.Age != null) employee.Result.Age = employeeUpdateDto.Age;
        // employee.Result.FileContent = employeeUpdateDto.Image.ContentType;
        if (employeeUpdateDto.Image != null)
        {
            employee.Result.FileExtension = employeeUpdateDto.Image?.ContentType;
            employee.Result.FileName = employeeUpdateDto.Image?.FileName;
            employee.Result.UserName = employee.Result.Email?.Substring(0, employee.Result.Email.IndexOf('@'));
            
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/employees", employee.Result.UserName!);
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            employee.Result.ImgUrl = path + "/" + employee.Result.FileName + ".png";
            await using FileStream fileStream = new(employee.Result.ImgUrl, FileMode.Create);
            employeeUpdateDto.Image?.CopyToAsync(fileStream);
        }

        return await employeeRepo.Update(employee.Result);
    }

    public async Task<int> Delete(int id)
    {
        var employee = employeeRepo.GetById(id);
        if (employee.Result != null) return await employeeRepo.Delete(employee.Result);
        return 0;
    }

    public EmployeeReadDto? Get(int id)
    {
        var employee = employeeRepo.GetById(id);
        if (employee.Result == null) return null;
        return new EmployeeReadDto()
        {
            Id = employee.Id,
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
        var employees = employeeRepo.GetAll();
        
        return  Task.FromResult(employees.Result.Select(employee => new EmployeeReadDto()
        {
            Id = employee.Id,
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

    public Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit)
    {
        var employees = employeeRepo.GetEmployeesAsync(term, sort, page, limit);
        return employees;

        #region commented

        // Task.FromResult(employees.Result.Employees);
        // .Employees.Select(employee => new PagedEmployeeResult()
        // {
        //     Id = employee.Id,
        //     FullName = employee.FullName,
        //     Phone = employee.Phone,
        //     Email = employee.Email,
        //     Age = employee.Age,
        //     JobPosition = employee.JobPosition,
        //     JoiningDate = employee.JoiningDate,
        //     YearsOfExperience = employee.YearsOfExperience,
        //     Salary = employee.Salary,
        //     DepartmentId = employee.DepartmentId,
        //     ManagerId = employee.ManagerId,
        //     ImgUrl = employee.ImgUrl
        // }));

        #endregion
    }

   

    public async Task<IEnumerable<EmployeeDto>> GetFilteredEmployeesAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await employeeRepo.GetAll();
        value2 ??= value1;

        var filteredResults = ApplyFilter(users, column, value1, operator1, value2)
            .Intersect(ApplyFilter(users, column, value2, operator2));

        var paginatedResults = filteredResults.Skip((page - 1) * pageSize).Take(pageSize);

        return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(paginatedResults);
    }
    private IEnumerable<Employee> ApplyFilter(IEnumerable<Employee> users, string column, string value, string? operatorType, string? value2 = null)
    {
        value2 ??= value;

        return operatorType?.ToLower() switch
        {
            "contains" => users.Where(e => e.GetValue(column).Contains(value)),
            "doesnotcontain" => users.SkipWhile(e => e.GetValue(column).Contains(value)),
            "startswith" => users.Where(e => e.GetValue(column).StartsWith(value)),
            "endswith" => users.Where(e => e.GetValue(column).EndsWith(value)),
            _ when decimal.TryParse(value, out var employeeValue) => ApplyNumericFilter(users, column, employeeValue, operatorType),
            _ => users
        };
    }

private IEnumerable<Employee> ApplyNumericFilter(IEnumerable<Employee> users, string column, decimal value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue == value),
        "neq" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue != value),
        "gte" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue >= value),
        "gt" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue > value),
        "lte" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue <= value),
        "lt" => users.Where(e => decimal.TryParse(e.GetValue(column), out var employeeValue) && employeeValue < value),
        _ => users
    };
}


    public Task<List<EmployeeDto>> GlobalSearch(string searchKey, string? column)
    {
        var  users = employeeRepo.GlobalSearch(searchKey);
        if(column!=null)
            users = (IQueryable<Employee>)employeeRepo.GetAll().Result.Where(e => e.GetValue(column).Contains(searchKey));
        var employees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(users);
        return Task.FromResult(employees.ToList());
    }


    // public string? GetFilePath(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.Result.EmployeeName!, file.Result.Name);
    // }
    
    public bool IsEmailUnique(string email)
    {
        var employee = employeeRepo.GetByEmail(email);
        return employee.Result == null;
    }

    public async Task<ManagerTree?> GetManagersTreeAsync(int employeeId)
{
    var employee = await employeeRepo.GetById(employeeId);

    if (employee == null)
    {
        return null;
    }

    return await BuildManagerTreeAsync(employee);
}

private async Task<ManagerTree?> BuildManagerTreeAsync(Employee employee)
{
    var node = new ManagerTree()
    {
        Employee = employee,
        Subordinates = new List<ManagerTree>()
    };

    var subordinates = await employeeRepo.GetSubordinatesAsync(employee.Id)!;

    foreach (var subordinate in subordinates)
    {
        var subordinateNode = await BuildManagerTreeAsync(subordinate);
        if (subordinateNode != null) node.Subordinates.Add(subordinateNode);
    }

    return node;
}

}

