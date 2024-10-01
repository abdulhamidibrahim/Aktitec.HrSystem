
using System.Security.Cryptography;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class EmployeeManager(
    IWebHostEnvironment webHostEnvironment,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IEmployeeManager
{
    
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
            PublicKey = RSA.Create(2048).ExportRSAPublicKeyPem(),
            PrivateKey = RSA.Create(2048).ExportRSAPrivateKeyPem(),
            FileExtension = image?.ContentType,
            UserName = employeeAddDto.Email?.Substring(0,employeeAddDto.Email.IndexOf('@')),
            CreatedAt = DateTime.Now,
        };
        
        #region commented

        // var file = new UserFile()
        // {
        //     FileName = employeeAddDto.Image.FileName,
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
        

       
        
         unitOfWork.Employee.Add(employee);
         if (employee.ManagerId.HasValue)
         {
             var manager = unitOfWork.Employee.GetById(employee.ManagerId.Value);

             if (manager != null && manager.Id == employee.Id)
             {
                 unitOfWork.Employee.Delete(employee);
                 // The employee cannot be the manager of their own manager
                 throw new InvalidOperationException("An employee cannot be the manager of their own manager.");
             }
         }

         return unitOfWork.SaveChangesAsync();
    }
    

    public Task<int> Update(EmployeeUpdateDto employeeUpdateDto, int id, IFormFile? image)
    {
        var employee = unitOfWork.Employee.GetById(id);
        
        if (employee == null) return Task.FromResult(0);
        if (employeeUpdateDto.FullName != null) employee.FullName = employeeUpdateDto.FullName;
        if (employeeUpdateDto.Phone != null) employee.Phone = employeeUpdateDto.Phone;
        if (employeeUpdateDto.Email != null) employee.Email = employeeUpdateDto.Email;
        if (employeeUpdateDto.DepartmentId != null) employee.DepartmentId = employeeUpdateDto.DepartmentId;
        if (employeeUpdateDto.JoiningDate != null) employee.JoiningDate = employeeUpdateDto.JoiningDate;
        if (employeeUpdateDto.Salary != null) employee.Salary = employeeUpdateDto.Salary;
        if (employeeUpdateDto.JobPosition != null) employee.JobPosition = employeeUpdateDto.JobPosition;
        if (employeeUpdateDto.YearsOfExperience != null) employee.YearsOfExperience = employeeUpdateDto.YearsOfExperience;
        if (employeeUpdateDto.ManagerId != null) employee.ManagerId = employeeUpdateDto.ManagerId;
        if (employeeUpdateDto.Gender != null) employee.Gender = employeeUpdateDto.Gender;
        if (employeeUpdateDto.Age != null) employee.Age = employeeUpdateDto.Age;
        // employee.Result.FileContent = employeeUpdateDto.Image.ContentType;
        if (image != null)
        {
            employee.FileExtension = image?.ContentType;
            employee.FileName = image?.FileName;
            employee.UserName = employee.Email?.Substring(0, employee.Email.IndexOf('@'));

            var unique = Guid.NewGuid();
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/employees", employee.UserName+unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            
           
            var imgPath = Path.Combine(path, employee.FileName!); 
            using FileStream fileStream = new(imgPath, FileMode.Create);
            image?.CopyToAsync(fileStream);
            employee.ImgUrl = Path.Combine("uploads/employees", employee.UserName+unique, employee.FileName!);
        }

        employee.UpdatedAt = DateTime.Now;
        unitOfWork.Employee.Update(employee);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var employee = unitOfWork.Employee.GetById(id);
        if (employee==null) return Task.FromResult(0);
        employee.IsDeleted = true;
        employee.DeletedAt = DateTime.Now;
        unitOfWork.Employee.Update(employee);
        return unitOfWork.SaveChangesAsync();
    }

    public EmployeeReadDto? Get(int id)
    {
        var employee = unitOfWork.Employee.GetById(id);
        if (employee == null) return new EmployeeReadDto();
        var manager =  unitOfWork.Employee.GetById(employee.ManagerId);
        return new EmployeeReadDto()
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
            ImgUrl = employee.ImgUrl,
            Manager = manager == null ? null : new EmployeeReadDto()
            {
                Id = manager.Id,
                FullName = manager.FullName,
                Phone = manager.Phone,
                Email = manager.Email,
                Age = manager.Age,
                JobPosition = manager.JobPosition,
                JoiningDate = manager.JoiningDate,
                YearsOfExperience = manager.YearsOfExperience,
                Salary = manager.Salary,
                DepartmentId = manager.DepartmentId,
                ManagerId = manager.ManagerId,
                ImgUrl = manager.ImgUrl
            }
        };
    }

    public Task<List<EmployeeReadDto>> GetAll()
    {
        var employees = unitOfWork.Employee.GetAll();
        
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
        var employees = unitOfWork.Employee.GetEmployeesAsync(term, sort, page, limit);
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
        var users = await unitOfWork.Employee.GetEmployeeWithDepartment();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var employeeDtos = paginatedResults.Select(e=> new EmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Phone = e.Phone,
                Email = e.Email,
                Age = e.Age,
                JobPosition = e.JobPosition,
                JoiningDate = e.JoiningDate,
                YearsOfExperience = e.YearsOfExperience,
                Salary = e.Salary,
                Gender = e.Gender,
                ImgUrl = e.ImgUrl,
                Department = e.Department?.Name,
                Manager = e.Manager?.FullName,
            });
          
            FilteredEmployeeDto result = new()
            {
                EmployeeDto = employeeDtos,
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

            var mappedEmployee = paginatedResults.Select(e=> new EmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Phone = e.Phone,
                Email = e.Email,
                Age = e.Age,
                JobPosition = e.JobPosition,
                JoiningDate = e.JoiningDate,
                YearsOfExperience = e.YearsOfExperience,
                Salary = e.Salary,
                Gender = e.Gender,
                ImgUrl = e.ImgUrl,
                Department = e.Department?.Name,
                Manager = e.Manager?.FullName,
            });

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
            IEnumerable<Employee> emp;
            emp = unitOfWork.Employee.GetEmployeeWithDepartment().Result
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var employeeDtos = emp.Select(e=> new EmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Phone = e.Phone,
                Email = e.Email,
                Age = e.Age,
                JobPosition = e.JobPosition,
                JoiningDate = e.JoiningDate,
                YearsOfExperience = e.YearsOfExperience,
                Salary = e.Salary,
                Gender = e.Gender,
                ImgUrl = e.ImgUrl,
                Department = e.Department?.Name,
                Manager = e.Manager?.FullName,
            });
            return Task.FromResult(employeeDtos.ToList());
        }

        var  emps = unitOfWork.Employee.GlobalSearch(searchKey);
        var employees = emps.Select(e=> new EmployeeDto()
        {
            Id = e.Id,
            FullName = e.FullName,
            Phone = e.Phone,
            Email = e.Email,
            Age = e.Age,
            JobPosition = e.JobPosition,
            JoiningDate = e.JoiningDate,
            YearsOfExperience = e.YearsOfExperience,
            Salary = e.Salary,
            Gender = e.Gender,
            ImgUrl = e.ImgUrl,
            Department = e.Department != null ? e.Department.Name : null,
            Manager = e.Manager != null ? e.Manager.FullName : null,
        });        return Task.FromResult(employees.ToList());
    }


    // public string? GetFilePath(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return Path.Combine(_webHostEnvironment.WebRootPath, "uploads/employees",file.Result.EmployeeName!, file.Result.FileName);
    // }
    
    public bool IsEmailUnique(string email)
    {
        var employee = unitOfWork.Employee.GetByEmail(email);
        return employee.Result == null;
    }
    
    public async Task<List<ManagerTree>> GetManagersTreeAsync()
    {
        var managers = await unitOfWork.Employee.GetAllManagersAsync();

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

    public async Task<int> AddEmployeesAsync(EmployeesAddDto employeeAddDtos)
    {
        foreach (var employee in employeeAddDtos.Employees)
        {
            var obj = new Employee()
            {
                FullName = employee.FullName!,
                Phone = employee.Phone,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                JoiningDate = employee.JoiningDate,
                Salary = employee.Salary,
                JobPosition = employee.JobPosition,
                YearsOfExperience = employee.YearsOfExperience,
                Gender = employee.Gender,
                ManagerId = employee.ManagerId,
                Age = employee.Age,
                UserName = employee.Email?.Substring(0,employee.Email.IndexOf('@')),
                CreatedAt = DateTime.Now,
            };
            unitOfWork.Employee.Add(obj);
        }
        return await unitOfWork.SaveChangesAsync();
    }

    private async Task<ManagerTree?> BuildManagerTreeAsync(Employee manager)
    {
        var node = new ManagerTree()
        {
            Employee = mapper.Map<Employee, EmployeeDto>(manager),
            Subordinates = new List<ManagerTree>()
        };

        var subordinates = await unitOfWork.Employee.GetSubordinatesAsync(manager.Id);

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

