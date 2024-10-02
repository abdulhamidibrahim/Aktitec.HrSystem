using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL.Managers.Company;

public class CompanyManager(IUnitOfWork unitOfWork,
                            UserUtility userUtility,
                            UserManager<ApplicationUser> userManager) : ICompanyManager
{
    public async Task<int> Add(CompanyAddDto companyAddDto)
    {
        var managerDto = companyAddDto.Manager;
      
        var companyDto = companyAddDto.Company;
        
        var company = new DAL.Models.Company()
        {
            CompanyName = companyDto.CompanyName,
            Email = companyDto.Email,
            Phone = companyDto.Phone,
            Address = companyDto.Address,
            Website = companyDto.Website,
            Fax = companyDto.Fax,
            Country = companyDto.Country,
            City = companyDto.City,
            Contact = companyDto.Contact,
            State = companyDto.State,
            Postal = companyDto.Postal,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId() ?? "",
        };

        // Create the ApplicationUser entity for the manager
        var manager = new ApplicationUser()
        {
            UserName = managerDto.UserName,
            Email = managerDto.Email,
            EmailConfirmed = true,
            CreatedAt = DateTime.Now,
            Password = managerDto.Password,
            CreatedBy = userUtility.GetUserId() ?? "",
            FirstName = managerDto.FirstName,
            LastName = managerDto.LastName,
            IsManager = true,
            HasAccess = true,
        };

        // Save the manager to the database and get the generated ID
        var created = await userManager.CreateAsync(manager, managerDto.Password);
        if (!created.Succeeded)
        {
            throw new Exception(created.Errors.FirstOrDefault()?.Description);
        }

        // Retrieve the ID of the newly created manager
        var managerId = manager.Id;

        // Set the manager for the company
        company.ManagerId = managerId;
        

        // Save the company to the database
        await unitOfWork.Company.Create(company);
        
        manager.TenantId = company.Id;
        // Commit the transaction
        await unitOfWork.SaveChangesAsync();

        return company.Id;

    }
    public async Task<int> AddAdmin(CompanyAddDto companyAddDto)
    {
        var managerDto = companyAddDto.Manager;
        var manager = new ApplicationUser()
        {
            UserName = managerDto.UserName,
            Email = managerDto.Email,
            EmailConfirmed = true,
            CreatedAt = DateTime.Now,
            Password = managerDto.Password,
            CreatedBy = userUtility.GetUserId()?? "",
            FirstName = managerDto.FirstName,
            LastName = managerDto.LastName,
            IsAdmin = true,
            HasAccess = true,
        };
        var created= userManager.CreateAsync(manager,managerDto.Password).Result;
        if (!created.Succeeded) throw new Exception(created.Errors.FirstOrDefault()?.Description);
        
        var companyDto = companyAddDto.Company;
        var company = new DAL.Models.Company()
        {
            CompanyName = companyDto.CompanyName,
            Email    = companyDto.Email,
            Phone = companyDto.Phone,
            Address = companyDto.Address,
            Website = companyDto.Website,
            Fax = companyDto.Fax,
            Country = companyDto.Country,
            City = companyDto.City,
            Contact = companyDto.Contact,
            State = companyDto.State,
            Postal = companyDto.Postal,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId()??"",
        };
        
        var companyId = await unitOfWork.Company.Create(company);
        
        manager.TenantId = companyId;
        
        await unitOfWork.SaveChangesAsync();
        
        return companyId;
    }

    public async Task<int> Update(CompanyUpdateDto companyUpdateDto, int id)
    {
        var company = unitOfWork.Company.GetById(id);
    
        if (company == null) return 0;
        var companyDto = companyUpdateDto.Company;
        // LogNote company properties
        company.CompanyName = companyDto.CompanyName;
        company.Email = companyDto.Email;
        company.Phone = companyDto.Phone;
        company.Address = companyDto.Address;
        company.Website = companyDto.Website;
        company.Fax = companyDto.Fax;
        company.Country = companyDto.Country;
        company.City = companyDto.City;
        company.Contact = companyDto.Contact;
        company.State = companyDto.State;
        company.Postal = companyDto.Postal;
        company.UpdatedAt = DateTime.Now;
        company.UpdatedBy = userUtility.GetUserId();

        // LogNote company in the repository
        unitOfWork.Company.Update(company);

        // LogNote the manager if needed
        var managerDto = companyUpdateDto.Manager;
        if (managerDto is not null)
        {
            var manager = await userManager.FindByEmailAsync(managerDto.Email);
            if (manager != null)
            {
                manager.UserName = managerDto.UserName;
                manager.Email = managerDto.Email;
                manager.FirstName = managerDto.FirstName;
                manager.LastName = managerDto.LastName;
                manager.UpdatedAt = DateTime.Now;
                manager.UpdatedBy = userUtility.GetUserId();

                var updateResult = await userManager.UpdateAsync(manager);
                if (!updateResult.Succeeded)
                {
                    throw new Exception(updateResult.Errors.FirstOrDefault()?.Description);
                }
            }
        }
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var company = unitOfWork.Company.GetById(id);
        if (company==null) return Task.FromResult(0);
        company.IsDeleted = true;
        company.DeletedAt = DateTime.Now;
        company.DeletedBy = userUtility.GetUserId();
        unitOfWork.Company.Update(company);
        return unitOfWork.SaveChangesAsync();
    }

    public  CompanyReadDto? Get(int id)
    {
        var company =  unitOfWork.Company.GetCompany(id);
        if (company == null) return null;
        
        var dto = new CompanyReadDto()
        {
            Company = new CompanyDto
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                Email = company.Email,
                Phone = company.Phone,
                Address = company.Address,
                Website = company.Website,
                Fax = company.Fax,
                Country = company.Country,
                City = company.City,
                Contact = company.Contact,
                State = company.State,
                Postal = company.Postal,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            },
            Manager = new UserDto()
            {
                Id = company.Manager.Id,
                UserName = company.Manager.UserName,
                Email = company.Manager.Email,
                FirstName = company.Manager.FirstName,
                LastName = company.Manager.LastName,
                Password = company.Manager.Password,
                CreatedAt = company.Manager.CreatedAt,
                CreatedBy =company.Manager.CreatedBy,
                UpdatedAt = company.Manager.UpdatedAt,
                UpdatedBy =company.Manager.UpdatedBy,
            }
        };
        return dto;
    }

    public async Task<IEnumerable<CompanyReadDto>> GetAll()
    {
        var company = await unitOfWork.Company.GetAllCompanies();
        var dto = company.Select(c=> new CompanyReadDto()
        {
            Company = new CompanyDto
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Website = c.Website,
                Fax = c.Fax,
                Country = c.Country,
                City = c.City,
                Contact = c.Contact,
                State = c.State,
                Postal = c.Postal,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            },
            
            Manager = new UserDto()
            {
                Id = c.Manager.Id,
                UserName = c.Manager.UserName,
                Email = c.Manager.Email,
                FirstName = c.Manager.FirstName,
                LastName = c.Manager.LastName,
                
                CreatedAt = c.Manager.CreatedAt,
                CreatedBy =c.Manager.CreatedBy,
                UpdatedAt = c.Manager.UpdatedAt,
                UpdatedBy =c.Manager.UpdatedBy,
            }

        });

        return dto;
    }

     public async Task<FilteredCompanyDto> GetFilteredCompaniesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var companies = await unitOfWork.Company.GetAllCompanies();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = companies.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var companyList = companies.ToList();

            var paginatedResults = companyList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = paginatedResults
                .Select(c => new CompanyReadDto()
                {
                    Company = new CompanyDto
                    {
                        Id = c.Id,
                        CompanyName = c.CompanyName,
                        Email = c.Email,
                        Phone = c.Phone,
                        Address = c.Address,
                        Website = c.Website,
                        Fax = c.Fax,
                        Country = c.Country,
                        City = c.City,
                        Contact = c.Contact,
                        State = c.State,
                        Postal = c.Postal,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.CreatedBy,
                        UpdatedAt = c.UpdatedAt,
                        UpdatedBy = c.UpdatedBy,
                    },
                    Manager = new UserDto()
                    {
                        Id = c.Manager.Id,
                        UserName = c.Manager.UserName,
                        Email = c.Manager.Email,
                        FirstName = c.Manager.FirstName,
                        LastName = c.Manager.LastName,
                        
                        CreatedAt = c.Manager.CreatedAt,
                        CreatedBy =c.Manager.CreatedBy,
                        UpdatedAt = c.Manager.UpdatedAt,
                        UpdatedBy =c.Manager.UpdatedBy,
                    }
                });

            FilteredCompanyDto result = new()
            {
                CompanyDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (companies != null)
        {
            IEnumerable<DAL.Models.Company> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(companies, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(companies, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedCompany = paginatedResults
                .Select(company => new CompanyReadDto()
                {
                    Company = new CompanyDto
                    {
                        Id = company.Id,
                        CompanyName = company.CompanyName,
                        Email = company.Email,
                        Phone = company.Phone,
                        Address = company.Address,
                        Website = company.Website,
                        Fax = company.Fax,
                        Country = company.Country,
                        City = company.City,
                        Contact = company.Contact,
                        State = company.State,
                        Postal = company.Postal,
                        CreatedAt = company.CreatedAt,
                        CreatedBy = company.CreatedBy,
                        UpdatedAt = company.UpdatedAt,
                        UpdatedBy = company.UpdatedBy,
                    },
                    Manager = new UserDto()
                    {
                        Id = company.Manager.Id,
                        UserName = company.Manager.UserName,
                        Email = company.Manager.Email,
                        FirstName = company.Manager.FirstName,
                        LastName = company.Manager.LastName,
                
                        CreatedAt = company.Manager.CreatedAt,
                        CreatedBy =company.Manager.CreatedBy,
                        UpdatedAt = company.Manager.UpdatedAt,
                        UpdatedBy =company.Manager.UpdatedBy,
                    }
                });

            FilteredCompanyDto filteredCompanyDto = new()
            {
                CompanyDto = mappedCompany,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredCompanyDto;
        }

        return new FilteredCompanyDto();
    }
    private IEnumerable<DAL.Models.Company> ApplyFilter(IEnumerable<DAL.Models.Company> companys, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => companys.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => companys.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => companys.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => companys.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var companyValue) => ApplyNumericFilter(companys, column, companyValue, operatorType),
            _ => companys
        };
    }

    private IEnumerable<DAL.Models.Company> ApplyNumericFilter(IEnumerable<DAL.Models.Company> companys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue == value),
        "neq" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue != value),
        "gte" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue >= value),
        "gt" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue > value),
        "lte" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue <= value),
        "lt" => companys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var companyValue) && companyValue < value),
        _ => companys
    };
}




    public async Task<List<CompanyReadDto>> GlobalSearch(string searchKey, string? column)
    {

        if (column != null)
        {
            IEnumerable<DAL.Models.Company> companyDto;
            companyDto = unitOfWork.Company.GetAllCompanies().Result.Where(e =>
                e.GetPropertyValue(column).ToLower().Contains(searchKey, StringComparison.OrdinalIgnoreCase));
            var company = companyDto.Select(company => new CompanyReadDto()
            {
                Company = new CompanyDto
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    Email = company.Email,
                    Phone = company.Phone,
                    Address = company.Address,
                    Website = company.Website,
                    Fax = company.Fax,
                    Country = company.Country,
                    City = company.City,
                    Contact = company.Contact,
                    State = company.State,
                    Postal = company.Postal,
                    CreatedAt = company.CreatedAt,
                    CreatedBy = company.CreatedBy,
                    UpdatedAt = company.UpdatedAt,
                    UpdatedBy = company.UpdatedBy,
                },
                Manager = new UserDto()
                {
                    Id = company.Manager.Id,
                    UserName = company.Manager.UserName,
                    Email = company.Manager.Email,
                    FirstName = company.Manager.FirstName,
                    LastName = company.Manager.LastName,
                
                    CreatedAt = company.Manager.CreatedAt,
                    CreatedBy =company.Manager.CreatedBy,
                    UpdatedAt = company.Manager.UpdatedAt,
                    UpdatedBy =company.Manager.UpdatedBy,
                }
            });

        return  (company.ToList());
        }

        var  companyDtos = unitOfWork.Company.GlobalSearch(searchKey);
        var companys = companyDtos.Select(company => new CompanyReadDto()
        {
            Company = new CompanyDto
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                Email = company.Email,
                Phone = company.Phone,
                Address = company.Address,
                Website = company.Website,
                Fax = company.Fax,
                Country = company.Country,
                City = company.City,
                Contact = company.Contact,
                State = company.State,
                Postal = company.Postal,
                CreatedAt = company.CreatedAt,
                CreatedBy = company.CreatedBy,
                UpdatedAt = company.UpdatedAt,
                UpdatedBy = company.UpdatedBy,
            },
            Manager = new UserDto()
            {
                Id = company.Manager.Id,
                UserName = company.Manager.UserName,
                Email = company.Manager.Email,
                FirstName = company.Manager.FirstName,
                LastName = company.Manager.LastName,
                
                CreatedAt = company.Manager.CreatedAt,
                CreatedBy =company.Manager.CreatedBy,
                UpdatedAt = company.Manager.UpdatedAt,
                UpdatedBy =company.Manager.UpdatedBy,
            }
        });
        return (companys.ToList());
    }

}