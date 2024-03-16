
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.BL.Helpers;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

    public Task<int> Add(EmployeeAddDto employeeAddDto, IFormFile? image)
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
            FileName = image?.FileName,
            FileExtension = image?.ContentType,
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

        var unique = Guid.NewGuid();

        var path = Path.Combine(webHostEnvironment.WebRootPath,"uploads/employees", employee.UserName+unique);
        if (Directory.Exists(path))
        {
            Directory.Delete(path,true);
        }else
        {
            Directory.CreateDirectory(path);
        }
        
        var imgPath = Path.Combine(path, employee.FileName!);
        using FileStream fileStream = new(imgPath, FileMode.Create);
        image?.CopyToAsync(fileStream);
        employee.ImgUrl = Path.Combine("uploads/employees", employee.UserName + unique, employee.FileName!);
        // _fileRepo.Add(file);
        

       
        
         var result = employeeRepo.Add(employee);
         if (employee.ManagerId.HasValue)
         {
             var manager = employeeRepo.GetById(employee.ManagerId.Value);

             if (manager != null && manager.Id == employee.Id)
             {
                 employeeRepo.Delete(employee);
                 // The employee cannot be the manager of their own manager
                 throw new InvalidOperationException("An employee cannot be the manager of their own manager.");
             }
         }

         return result;
    }
    

    public async Task<int> Update(EmployeeUpdateDto employeeUpdateDto,int id, IFormFile? image)
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
        if (image != null)
        {
            employee.Result.FileExtension = image?.ContentType;
            employee.Result.FileName = image?.FileName;
            employee.Result.UserName = employee.Result.Email?.Substring(0, employee.Result.Email.IndexOf('@'));

            var unique = Guid.NewGuid();
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/employees", employee.Result.UserName+unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            
           
            var imgPath = Path.Combine(path, employee.Result.FileName!);
            await using FileStream fileStream = new(imgPath, FileMode.Create);
            image?.CopyToAsync(fileStream);
            employee.Result.ImgUrl = Path.Combine("uploads/employees", employee.Result.UserName+unique, employee.Result.FileName!);
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
        var manager = employeeRepo.GetById(employee.Result.ManagerId);
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
            Manager = manager.Result == null ? null : new EmployeeReadDto()
            {
                Id = manager.Result.Id,
                FullName = manager.Result.FullName,
                Phone = manager.Result.Phone,
                Email = manager.Result.Email,
                Age = manager.Result.Age,
                JobPosition = manager.Result.JobPosition,
                JoiningDate = manager.Result.JoiningDate,
                YearsOfExperience = manager.Result.YearsOfExperience,
                Salary = manager.Result.Salary,
                DepartmentId = manager.Result.DepartmentId,
                ManagerId = manager.Result.ManagerId,
                ImgUrl = manager.Result.ImgUrl
            }
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

   

    public async Task<FilteredEmployeeDto> GetFilteredEmployeesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await employeeRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(paginatedResults);
            FilteredEmployeeDto result = new()
            {
                EmployeeDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Employee> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(users, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedEmployee = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(paginatedResults);

            FilteredEmployeeDto filteredEmployeeDto = new()
            {
                EmployeeDto = mappedEmployee,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredEmployeeDto;
        }

        return new FilteredEmployeeDto();
    }
    private IEnumerable<Employee> ApplyFilter(IEnumerable<Employee> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var employeeValue) => ApplyNumericFilter(users, column, employeeValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Employee> ApplyNumericFilter(IEnumerable<Employee> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var employeeValue) && employeeValue < value),
        _ => users
    };
}


    public Task<List<EmployeeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Employee> user;
            user = employeeRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var employee = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(user);
            return Task.FromResult(employee.ToList());
        }

        var  users = employeeRepo.GlobalSearch(searchKey);
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
    
    public async Task<List<ManagerTree>> GetManagersTreeAsync()
    {
        var managers = await employeeRepo.GetAllManagersAsync();

        if (!managers.Any())
        {
            return new List<ManagerTree>();
        }

        var managerTrees = new List<ManagerTree>();

        foreach (var manager in managers)
        {
            var managerTree = await BuildManagerTreeAsync(manager);
            if (managerTree != null)
            {
                managerTrees.Add(managerTree);
            }
        }

        return managerTrees;
    }

    private async Task<ManagerTree?> BuildManagerTreeAsync(Employee manager)
    {
        var node = new ManagerTree()
        {
            Employee = mapper.Map<Employee, EmployeeDto>(manager),
            Subordinates = new List<ManagerTree>()
        };

        var subordinates = await employeeRepo.GetSubordinatesAsync(manager.Id);

        foreach (var subordinate in subordinates)
        {
            var subordinateNode = await BuildManagerTreeAsync(subordinate);
            if (subordinateNode != null)
            {
                node.Subordinates.Add(subordinateNode);
            }
        }

        return node;
    }



//     public async Task<ManagerTree?> GetManagersTreeAsync()
// {
//     var employees = await employeeRepo.GetAllManagers();
//
//     // if (employees == null)
//     // {
//     //     return null;
//     // }
//     var dtos=mapper.Map<List<Employee>, List<EmployoeeDto>>(employees);
//     
//     foreach (var employee in dtos)
//     {
//         List<Employee> subordinates = await employeeRepo.GetSubordinatesAsync(employee.Id);
//         var employeeSubordinates = mapper.Map<List<Employee>, List<EmployoeeDto>>(subordinates);
//         return await BuildManagerTreeAsync(employee,employeeSubordinates);
//     }
//
//     
// }
//
// private async Task<ManagerTree?> BuildManagerTreeAsync(EmployoeeDto employee,List<EmployoeeDto> employees)
// {
//     var node = new ManagerTree()
//     {
//         Employee = employee,
//         Subordinates = new List<ManagerTree>()
//     };
//     
//     // var employees = await employeeRepo.GetSubordinatesAsync(employee.Id);
//     foreach (var subordinate in employees)
//     {
//         var subordinateNode = await BuildManagerTreeAsync(subordinate,employees);
//         if (subordinateNode != null) node.Subordinates.Add(subordinateNode);
//     }
//
//     return node;
// }

// {
//     var node = new ManagerTree()
//     {
//         Employee = employee,
//         Subordinates = new List<ManagerTree>()
//     };
//     
//     
//     var subordinates = await employeeRepo.GetSubordinatesAsync(employee.Id)!;
//
//     foreach (var subordinate in subordinates)
//     {
//         var subordinateNode = await BuildManagerTreeAsync(subordinate);
//         if (subordinateNode != null) node.Subordinates.Add(subordinateNode);
//     }
//
//     return node;
// }

}

