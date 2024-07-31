
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class LicenseManager(
    IUnitOfWork unitOfWork) : ILicenseManager
{
    public Task<int> Add(LicenseAddDto licenseAddDto)
    {
        var license = new License()
        {
          StartDate = licenseAddDto.StartDate,
          EndDate = licenseAddDto.EndDate,
          Price = licenseAddDto.Price,
          Active = licenseAddDto.Active,
          CreatedAt = DateTime.Now,
          CreatedBy = UserUtility.GetUserName(),
        };
        
        unitOfWork.License.Add(license);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(LicenseUpdateDto licenseUpdateDto, int id)
    {
        var license = unitOfWork.License.GetById(id);

        if (licenseUpdateDto.StartDate.Date != null) license.StartDate = licenseUpdateDto.StartDate;
        if (licenseUpdateDto.EndDate!=null) license.EndDate = licenseUpdateDto.EndDate;
        if (licenseUpdateDto.Price != null) license.Price = licenseUpdateDto.Price;
        if (licenseUpdateDto.Active != null) license.Active = licenseUpdateDto.Active;
        if (licenseUpdateDto.CompanyId !=0 ) license.CompanyId = licenseUpdateDto.CompanyId;

        if (license == null) return Task.FromResult(0);
        
        license.UpdatedAt = DateTime.Now;
        license.UpdatedBy = UserUtility.GetUserName();
        
        unitOfWork.License.Update(license);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var license = unitOfWork.License.GetById(id);
        if (license==null) return Task.FromResult(0);
        license.IsDeleted = true;
        license.DeletedAt = DateTime.Now;
        license.DeletedBy = UserUtility.GetUserName();
        unitOfWork.License.Update(license);
        return unitOfWork.SaveChangesAsync();
    }

    public LicenseReadDto? Get(int id)
    {
        var license = unitOfWork.License.GetById(id);
        if (license == null) return null;
        
        return new LicenseReadDto()
        {
            Id = license.Id,
            StartDate = license.StartDate,
            EndDate = license.EndDate,
            Price = license.Price,
            Active = license.Active,
            CompanyId = license.CompanyId,
        };
    }

    public async Task<List<LicenseReadDto>> GetAll()
    {
        var license = await unitOfWork.License.GetAll();
        return license.Select(license => new LicenseReadDto()
        {
            Id = license.Id,
            StartDate = license.StartDate,
            EndDate = license.EndDate,
            Price = license.Price,
            Active = license.Active,
            CompanyId = license.CompanyId,
            
        }).ToList();
    }

   

    public async Task<FilteredLicensesDto> GetFilteredLicenseAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var licenseList = await unitOfWork.License.GetAllLicenses();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = licenseList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var policyList = licenseList.ToList();

            var paginatedResults = policyList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<LicenseDto>();
            foreach (var license in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new LicenseDto()
                {
                    Id = license.Id,
                    StartDate = license.StartDate,
                    EndDate = license.EndDate,
                    Price = license.Price,
                    Active = license.Active,
                    CompanyId = license.CompanyId,
                });
            }
            FilteredLicensesDto filteredBudgetsExpensesDto = new()
            {
                LicenseDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (licenseList != null)
        {
            IEnumerable<License> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(licenseList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(licenseList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var licenses = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(licenses);
            
            var mappedLicense = new List<LicenseDto>();

            foreach (var license in paginatedResults)
            {
                
                mappedLicense.Add(new LicenseDto()
                {
                    Id = license.Id,
                    StartDate = license.StartDate,
                    EndDate = license.EndDate,
                    Price = license.Price,
                    Active = license.Active,
                    CompanyId = license.CompanyId,
                });
            }
            FilteredLicensesDto filteredLicenseDto = new()
            {
                LicenseDto = mappedLicense,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredLicenseDto;
        }

        return new FilteredLicensesDto();
    }
    private IEnumerable<License> ApplyFilter(IEnumerable<License> policys, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => policys.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var licenseValue) => ApplyNumericFilter(policys, column, licenseValue, operatorType),
            _ => policys
        };
    }

    private IEnumerable<License> ApplyNumericFilter(IEnumerable<License> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var licenseValue) && licenseValue < value),
        _ => policys
    };
}


    public Task<List<LicenseDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<License> policy;
            policy = unitOfWork.License.GetAllLicenses().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var license = policy.Select(license => new LicenseDto()
            {
                Id = license.Id,
                StartDate = license.StartDate,
                EndDate = license.EndDate,
                Price = license.Price,
                Active = license.Active,
                CompanyId = license.CompanyId,
            });
            return Task.FromResult(license.ToList());
        }

        var  policys = unitOfWork.License.GlobalSearch(searchKey);
        var licenses = policys.Select(license => new LicenseDto()
        {
            Id = license.Id,
            StartDate = license.StartDate,
            EndDate = license.EndDate,
            Price = license.Price,
            Active = license.Active,
            CompanyId = license.CompanyId,
        });
        return Task.FromResult(licenses.ToList());
    }

}
