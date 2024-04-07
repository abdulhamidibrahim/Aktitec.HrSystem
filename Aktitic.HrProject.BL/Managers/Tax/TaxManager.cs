
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TaxManager:ITaxManager
{
    private readonly ITaxRepo _taxRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TaxManager(ITaxRepo taxRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _taxRepo = taxRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
    }
    
    public Task<int> Add(TaxAddDto taxAddDto)
    {
        var tax = new Tax()
        {
            Name = taxAddDto.Name,
            Percentage = taxAddDto.Percentage,
            Status = taxAddDto.Status,
        }; 
        _taxRepo.Add(tax);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaxUpdateDto taxUpdateDto, int id)
    {
        var tax = _taxRepo.GetById(id);
        
        
        if (tax == null) return Task.FromResult(0);
        
        if(taxUpdateDto.Name != null) tax.Name = taxUpdateDto.Name;
        if(taxUpdateDto.Percentage != null) tax.Percentage = taxUpdateDto.Percentage;
        if(taxUpdateDto.Status != null) tax.Status = taxUpdateDto.Status;
       
        
        _taxRepo.Update(tax);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _taxRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public TaxReadDto? Get(int id)
    {
        var tax = _taxRepo.GetById(id);
        if (tax == null) return null;
        return new TaxReadDto()
        {
            Id = tax.Id,
            Name = tax.Name,
            Percentage = tax.Percentage,
            Status = tax.Status,
        };
    }

    public Task<List<TaxReadDto>> GetAll()
    {
        var tax = _taxRepo.GetAll();
        return Task.FromResult(tax.Result.Select(p => new TaxReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            Percentage = p.Percentage,
            Status = p.Status,

        }).ToList());
    }
    
     public async Task<FilteredTaxDto> GetFilteredTaxsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _taxRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedTaxs = new List<TaxDto>();
            foreach (var tax in paginatedResults)
            {
                mappedTaxs.Add(new TaxDto()
                {
                    Id = tax.Id,
                    Name = tax.Name,
                    Percentage = tax.Percentage,
                    Status = tax.Status,
                });
            }
            FilteredTaxDto filteredTaxDto = new()
            {
                TaxDto = mappedTaxs,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredTaxDto;
        }

        if (users != null)
        {
            IEnumerable<Tax> filteredResults;
        
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

            // var taxs = paginatedResults.ToList();
            // var mappedTax = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(taxs);
            
            var mappedTaxs = new List<TaxDto>();

            foreach (var tax in paginatedResults)
            {
                
                mappedTaxs.Add(new TaxDto()
                {
                    Id = tax.Id,
                    Name = tax.Name,
                    Percentage = tax.Percentage,
                    Status = tax.Status,
                    
                });
            }
            FilteredTaxDto filteredTaxDto = new()
            {
                TaxDto = mappedTaxs,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTaxDto;
        }

        return new FilteredTaxDto();
    }
    private IEnumerable<Tax> ApplyFilter(IEnumerable<Tax> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var taxValue) => ApplyNumericFilter(users, column, taxValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Tax> ApplyNumericFilter(IEnumerable<Tax> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue < value),
        _ => users
    };
}


    public Task<List<TaxDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Tax> user;
            user = _taxRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var tax = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(user);
            return Task.FromResult(tax.ToList());
        }

        var  users = _taxRepo.GlobalSearch(searchKey);
        var taxs = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(users);
        return Task.FromResult(taxs.ToList());
    }

}
