
using System.Security.Cryptography;
using System.Text;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ApplicationUserManager(
    IWebHostEnvironment webHostEnvironment,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    UserUtility userUtility,
    UserManager<ApplicationUser> userManager)
    : IApplicationUserManager
{


    public ApplicationUser Add(ApplicationUserAddDto applicationUserAddDto)
    {
       // var permissions = JsonConvert.DeserializeObject<List<PermissionsDto>>(applicationUserAddDto.Permissions!);
       // var mappedPermissions = mapper.Map<List<PermissionsDto>, List<Permission>>(permissions);
        var applicationUser = new ApplicationUser()
        {
            PhoneNumber = applicationUserAddDto.Phone,
            Email = applicationUserAddDto.Email,
            FirstName = applicationUserAddDto.FirstName,
            RoleId = applicationUserAddDto.Role,
            TenantId = applicationUserAddDto.CompanyId,
            LastName = applicationUserAddDto.LastName,
            EmployeeId = applicationUserAddDto.EmployeeId,
            // Permissions = mappedPermissions,
            Password = applicationUserAddDto.Password,
            Date = applicationUserAddDto.Date,
            UserName = applicationUserAddDto.UserName,
            EmailConfirmed = false,
            CreatedBy = userUtility.GetUserName() ?? string.Empty,
            // UserName = applicationUserAddDto.Email?.Substring(0, applicationUserAddDto.Email.IndexOf('@'))
        };

        if (applicationUserAddDto.Image != null)
        {
            var unique = Guid.NewGuid();

            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/users", applicationUser.UserName + unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            var imgPath = Path.Combine(path, applicationUserAddDto.Image?.FileName!);
            using FileStream fileStream = new(imgPath, FileMode.Create);
            applicationUserAddDto.Image?.CopyToAsync(fileStream);
            applicationUser.Image = Path.Combine("uploads/applicationUsers", applicationUser.UserName + unique,
                applicationUserAddDto.Image?.FileName!);
            // _fileRepo.Add(file);

        }


        // unitOfWork.ApplicationUser.Add(applicationUser);
        return applicationUser;
    }
     public ApplicationUser Create(ApplicationUserAddDto applicationUserAddDto)
    {
       // var permissions = JsonConvert.DeserializeObject<List<PermissionsDto>>(applicationUserAddDto.Permissions!);
       // var mappedPermissions = mapper.Map<List<PermissionsDto>, List<Permission>>(permissions);
        var applicationUser = new ApplicationUser()
        {
            PhoneNumber = applicationUserAddDto.Phone,
            Email = applicationUserAddDto.Email,
            FirstName = applicationUserAddDto.FirstName,
            RoleId = applicationUserAddDto.Role,
            TenantId = int.Parse(userUtility.GetCurrentCompany()),
            LastName = applicationUserAddDto.LastName,
            EmployeeId = applicationUserAddDto.EmployeeId,
            // Permissions = mappedPermissions,
            HasAccess = true,
            Password = applicationUserAddDto.Password,
            Date = applicationUserAddDto.Date,
            UserName = applicationUserAddDto.UserName,
            EmailConfirmed = true,
            CreatedBy = userUtility.GetUserName() ?? string.Empty,
            
            
            State = applicationUserAddDto.State,
            Country = applicationUserAddDto.Country,
            PinCode = applicationUserAddDto.PinCode,
            Birthday = applicationUserAddDto.Birthday,
            Address = applicationUserAddDto.Address,
            Gender = applicationUserAddDto.Gender,
            PassportNumber = applicationUserAddDto.PassportNumber,
            PassportExpDate = applicationUserAddDto.PassportExpDate,
            Tel = applicationUserAddDto.Tel,
            Nationality = applicationUserAddDto.Nationality,
            Religion = applicationUserAddDto.Religion,
            MatritalStatus = applicationUserAddDto.MatritalStatus,
            EmploymentSpouse = applicationUserAddDto.EmploymentSpouse,
            ChildrenNumber = applicationUserAddDto.ChildrenNumber,
            ReportsToId = applicationUserAddDto.ReportsTo,
            
    
            // UserName = applicationUserAddDto.Email?.Substring(0, applicationUserAddDto.Email.IndexOf('@'))
        };

        if (applicationUserAddDto.Image != null)
        {
            var unique = Guid.NewGuid();

            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/users", applicationUser.UserName + unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            var imgPath = Path.Combine(path, applicationUserAddDto.Image?.FileName!);
            using FileStream fileStream = new(imgPath, FileMode.Create);
            applicationUserAddDto.Image?.CopyToAsync(fileStream);
            applicationUser.Image = Path.Combine("uploads/users", applicationUser.UserName + unique,applicationUserAddDto.Image?.FileName!);
            // _fileRepo.Add(file);

        }


        // unitOfWork.ApplicationUser.Add(applicationUser);
        // return unitOfWork.SaveChangesAsync();
        return applicationUser;
    }
    

    public async Task<int> Update(ApplicationUserUpdateDto applicationUserUpdateDto, int id)
    {
        var applicationUser = unitOfWork.ApplicationUser.GetById(id);
        if (applicationUser == null)
            return 0;

        // Deserialize permissions
        // var permissions = JsonConvert.DeserializeObject<List<PermissionsDto>>(applicationUserUpdateDto.Permissions!);
        
        
        // LogNote applicationUser properties
        applicationUser.FirstName = applicationUserUpdateDto.FirstName;
        
        applicationUser.PhoneNumber = applicationUserUpdateDto.Phone;
        
        if (!applicationUserUpdateDto.Email.IsNullOrEmpty())
            applicationUser.Email = applicationUserUpdateDto.Email;
        
        applicationUser.LastName = applicationUserUpdateDto.LastName;
        
        if (applicationUserUpdateDto.Role != 0)
            applicationUser.RoleId = applicationUserUpdateDto.Role;
        
        if (applicationUserUpdateDto.ClientId is not null && applicationUserUpdateDto.ClientId != 0)
            applicationUser.ClientId = applicationUserUpdateDto.ClientId;
        
        if (applicationUserUpdateDto.CompanyId != 0)
            if (applicationUserUpdateDto.CompanyId != null)
                applicationUser.TenantId = (int)applicationUserUpdateDto.CompanyId;

        if (applicationUserUpdateDto.EmployeeId != null)
            applicationUser.EmployeeId = applicationUserUpdateDto.EmployeeId;
        
        if (!applicationUserUpdateDto.Password.IsNullOrEmpty())
        {
            applicationUser.Password = applicationUserUpdateDto.Password;

            if (!applicationUserUpdateDto.Email.IsNullOrEmpty())
            {
                var user = userManager.FindByEmailAsync(applicationUserUpdateDto.Email);

                if (user.Result != null)
                {
                    var token = userManager.GeneratePasswordResetTokenAsync(user.Result);
                    await userManager.ResetPasswordAsync(user.Result,token.Result, applicationUserUpdateDto.Password);
                }
            }
        }        
        
        if (applicationUserUpdateDto.Date != applicationUser.Date)
            applicationUser.Date = applicationUserUpdateDto.Date;
        
        if (!applicationUserUpdateDto.UserName.IsNullOrEmpty())
            applicationUser.UserName = applicationUserUpdateDto.UserName;
        
        applicationUser.UpdatedAt = DateTime.Now;
        applicationUser.UpdatedBy = userUtility.GetUserName();
        
        if(!applicationUserUpdateDto.State.IsNullOrEmpty())
            applicationUser.State = applicationUserUpdateDto.State;
        if(!applicationUserUpdateDto.Country.IsNullOrEmpty())
            applicationUser.Country = applicationUserUpdateDto.Country;
        if(!applicationUserUpdateDto.PinCode.IsNullOrEmpty())
            applicationUser.PinCode = applicationUserUpdateDto.PinCode;
        if(!applicationUserUpdateDto.Birthday.Equals(DateTime.MinValue))
            applicationUser.Birthday = applicationUserUpdateDto.Birthday;
        if(!applicationUserUpdateDto.Address.IsNullOrEmpty())
            applicationUser.Address = applicationUserUpdateDto.Address;
        if(applicationUserUpdateDto.Gender != applicationUser.Gender)
            applicationUser.Gender = applicationUserUpdateDto.Gender;
        if(!applicationUserUpdateDto.PassportNumber.IsNullOrEmpty())
            applicationUser.PassportNumber = applicationUserUpdateDto.PassportNumber;
        if(!applicationUserUpdateDto.PassportExpDate.Equals(DateTime.MinValue))
            applicationUser.PassportExpDate = applicationUserUpdateDto.PassportExpDate;
        if(!applicationUserUpdateDto.Tel.IsNullOrEmpty())
            applicationUser.Tel = applicationUserUpdateDto.Tel;
        if(!applicationUserUpdateDto.Nationality.IsNullOrEmpty())
            applicationUser.Nationality = applicationUserUpdateDto.Nationality;
        if(!applicationUserUpdateDto.Religion.IsNullOrEmpty())
            applicationUser.Religion = applicationUserUpdateDto.Religion;
        if(!applicationUserUpdateDto.MatritalStatus.IsNullOrEmpty())
            applicationUser.MatritalStatus = applicationUserUpdateDto.MatritalStatus;
        if(!applicationUserUpdateDto.EmploymentSpouse.IsNullOrEmpty())
            applicationUser.EmploymentSpouse = applicationUserUpdateDto.EmploymentSpouse;
        if(applicationUserUpdateDto.ChildrenNumber != applicationUser.ChildrenNumber) 
            applicationUser.ChildrenNumber = applicationUserUpdateDto.ChildrenNumber;
        if(!applicationUserUpdateDto.ReportsTo.Equals(0))
            applicationUser.ReportsToId = applicationUserUpdateDto.ReportsTo;
        
        
        if (applicationUserUpdateDto?.Image != null)
        {
        
            var unique = Guid.NewGuid();
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/users", applicationUser.UserName + unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            var imgPath = Path.Combine(path, applicationUserUpdateDto.Image.FileName);
            await using FileStream fileStream = new(imgPath, FileMode.Create);
            await applicationUserUpdateDto.Image.CopyToAsync(fileStream);
            applicationUser.Image = Path.Combine("uploads/users", applicationUser.UserName + unique, applicationUserUpdateDto.Image.FileName);
        }
        unitOfWork.ApplicationUser.Update(applicationUser);
        return await unitOfWork.SaveChangesAsync();
    }


    public Task<int> Delete(int id)
    {

        var applicationUser = unitOfWork.ApplicationUser.GetById(id);

        if (applicationUser == null) return Task.FromResult(0);
        
        applicationUser.IsDeleted = true;
        
        unitOfWork.ApplicationUser.Update(applicationUser);
        
        return unitOfWork.SaveChangesAsync();
    }

    public ApplicationUserReadDto? Get(int id)
    {
        var applicationUser = unitOfWork.ApplicationUser.GetById(id);
        var permissions = unitOfWork.Permission.GetByApplicationUserId(id);
        var mappedPermissions = mapper.Map<List<Permission>, List<PermissionsDto>>(permissions);
        if (applicationUser == null) return null;
        return new ApplicationUserReadDto()
        {
            Id = applicationUser.Id,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Role= applicationUser.RoleId,
            Email = applicationUser.Email,
            CompanyId = applicationUser.TenantId,
            Image = applicationUser.Image,
            EmployeeId = applicationUser.EmployeeId,
            Password = applicationUser.Password,
            UserName = applicationUser.UserName,
            Phone = applicationUser.PhoneNumber,
            Date = applicationUser.Date,
           
        };
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await unitOfWork.ApplicationUser.FindByEmailAsync(email);
    }

    public Task<List<ApplicationUserReadDto>> GetAll()
    {
        var applicationUsers = unitOfWork.ApplicationUser.GetAll();
        
        return  Task.FromResult(applicationUsers.Result.Select(applicationUser => new ApplicationUserReadDto()
        {
            Id = applicationUser.Id,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Phone = applicationUser.PhoneNumber,
            Email = applicationUser.Email,
            CompanyId = applicationUser.TenantId,
            Image = applicationUser.Image,
            Role= applicationUser.RoleId,
            UserName = applicationUser.UserName,
            EmployeeId = applicationUser.EmployeeId,
        }).ToList());
    }

    // public Task<PagedApplicationUserResult> GetApplicationUsersAsync(string? term, string? sort, int page, int limit)
    // {
    //     var applicationUsers = unitOfWork.ApplicationUser.GetApplicationUsersAsync(term, sort, page, limit);
    //     return applicationUsers;
    // }

   

    public async Task<FilteredApplicationUserDto> GetFilteredApplicationUsersAsync(int companyId,string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var ApplicationUsers = await unitOfWork.ApplicationUser.GetAllWithEmployeesAsync(companyId);
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = ApplicationUsers.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var ApplicationUserList = ApplicationUsers.ToList();

            var paginatedResults = ApplicationUserList.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedApplicationUsers = new List<ApplicationUserDto>();

            foreach (var applicationUser in paginatedResults)
            {
                mappedApplicationUsers.Add(new ApplicationUserDto()
                {
                    Id = applicationUser.Id,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    UserName = applicationUser.UserName,
                    Email = applicationUser.Email,
                    Phone = applicationUser.PhoneNumber,
                    CompanyId = applicationUser.TenantId,
                    Role = applicationUser.RoleId,
                    Image = applicationUser.Image,
                    Date = applicationUser.Date,
                    EmployeeId = applicationUser.EmployeeId,
                    Employee = mapper.Map<Employee,EmployeeDto>(applicationUser.Employee) ?? null,
                    // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(applicationUser.Permissions.ToList())
                    
                });
            }
            
            FilteredApplicationUserDto result = new()
            {
                ApplicationUserDto = mappedApplicationUsers,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (ApplicationUsers != null)
        {
            IEnumerable<ApplicationUser> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(ApplicationUsers, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(ApplicationUsers, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedApplicationUsers = new List<ApplicationUserDto>();

            foreach (var user in paginatedResults)
            {
                mappedApplicationUsers.Add(new ApplicationUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName?? string.Empty,
                    Email = user.Email?? string.Empty,
                    Phone = user.PhoneNumber?? string.Empty,
                    CompanyId= user.TenantId,
                    Role = user.RoleId,
                    Image = user.Image,
                    Date = user.Date,
                    EmployeeId = user.EmployeeId,
                    Employee = mapper.Map<Employee,EmployeeDto>(user?.Employee) ?? null,
                    // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(user.Permissions.ToList())

                });
            }
            
            FilteredApplicationUserDto result = new()
            {
                ApplicationUserDto = mappedApplicationUsers,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return result;
        }

        return new FilteredApplicationUserDto();
    }
    private IEnumerable<ApplicationUser> ApplyFilter(IEnumerable<ApplicationUser> ApplicationUsers, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => ApplicationUsers.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => ApplicationUsers.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => ApplicationUsers.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => ApplicationUsers.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var applicationUserValue) => ApplyNumericFilter(ApplicationUsers, column, applicationUserValue, operatorType),
            _ => ApplicationUsers
        };
    }

private IEnumerable<ApplicationUser> ApplyNumericFilter(IEnumerable<ApplicationUser> ApplicationUsers, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue == value),
        "neq" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue != value),
        "gte" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue >= value),
        "gt" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue > value),
        "lte" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue <= value),
        "lt" => ApplicationUsers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var applicationUserValue) && applicationUserValue < value),
        _ => ApplicationUsers
    };
}


    public Task<List<ApplicationUserDto>> GlobalSearch(int companyId, string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<ApplicationUser> enumerable = unitOfWork.ApplicationUser.GetAllWithEmployeesAsync(companyId).Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var applicationUser = enumerable.Select(user => new ApplicationUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName?? string.Empty,
                Email = user.Email?? string.Empty,
                Phone = user.PhoneNumber?? string.Empty,
                CompanyId = user.TenantId,
                Role = user.RoleId,
                Image = user.Image,
                Date = user.Date,
                EmployeeId = user.EmployeeId,
                Employee = mapper.Map<Employee,EmployeeDto>(user?.Employee) ?? null,
                // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(user.Permissions.ToList())

            });
            return Task.FromResult(applicationUser.ToList());
        }

        var  globalSearch = unitOfWork.ApplicationUser.GlobalSearch(searchKey);
        var applicationUsers = globalSearch.Select(user => new ApplicationUserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName?? string.Empty,
            Email = user.Email?? string.Empty,
            Phone = user.PhoneNumber?? string.Empty,
            CompanyId = user.TenantId,
            Role = user.RoleId,
            Image = user.Image,
            Date = user.Date,
            EmployeeId = user.EmployeeId,
            Employee = mapper.Map<Employee,EmployeeDto>(user.Employee) ?? null,
            // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(user.Permissions.ToList())

        });
        return Task.FromResult(applicationUsers.ToList());
    }
    
}

public static class HashingHelper
{
    
    public static string HashPassword(this string password)
    {
        using SHA256 sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }
}

