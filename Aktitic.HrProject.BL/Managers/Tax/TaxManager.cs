using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TaxManager:ITaxManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TaxManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TaxAddDto taxAddDto)
    {
        var tax = new Tax()
        {
            Name = taxAddDto.Name,
            Percentage = taxAddDto.Percentage,
            Status = taxAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Tax.Add(tax);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaxUpdateDto taxUpdateDto, int id)
    {
        var tax = _unitOfWork.Tax.GetById(id);
        
        
        if (tax == null) return Task.FromResult(0);
        
        if(taxUpdateDto.Name != null) tax.Name = taxUpdateDto.Name;
        if(taxUpdateDto.Percentage != null) tax.Percentage = taxUpdateDto.Percentage;
        if(taxUpdateDto.Status != null) tax.Status = taxUpdateDto.Status;

        tax.UpdatedAt = DateTime.Now;
        _unitOfWork.Tax.Update(tax);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var tax = _unitOfWork.Tax.GetById(id);
        if (tax==null) return Task.FromResult(0);
        tax.IsDeleted = true;
        tax.DeletedAt = DateTime.Now;
        _unitOfWork.Tax.Update(tax);
        return _unitOfWork.SaveChangesAsync();
    }

    public TaxReadDto? Get(int id)
    {
        var tax = _unitOfWork.Tax.GetById(id);
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
        var tax = _unitOfWork.Tax.GetAll();
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
        var taxs = await _unitOfWork.Tax.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = taxs.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var taxList = taxs.ToList();

            var paginatedResults = taxList.Skip((page - 1) * pageSize).Take(pageSize);
    
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

        if (taxs != null)
        {
            IEnumerable<Tax> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(taxs, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(taxs, column, value2, operator2));
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
    private IEnumerable<Tax> ApplyFilter(IEnumerable<Tax> taxs, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => taxs.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => taxs.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => taxs.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => taxs.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var taxValue) => ApplyNumericFilter(taxs, column, taxValue, operatorType),
            _ => taxs
        };
    }

    private IEnumerable<Tax> ApplyNumericFilter(IEnumerable<Tax> taxs, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue == value),
        "neq" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue != value),
        "gte" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue >= value),
        "gt" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue > value),
        "lte" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue <= value),
        "lt" => taxs.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taxValue) && taxValue < value),
        _ => taxs
    };
}


    public Task<List<TaxDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Tax> taxDto;
            taxDto = _unitOfWork.Tax.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var tax = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(taxDto);
            return Task.FromResult(tax.ToList());
        }

        var  taxDtos = _unitOfWork.Tax.GlobalSearch(searchKey);
        var taxs = _mapper.Map<IEnumerable<Tax>, IEnumerable<TaxDto>>(taxDtos);
        return Task.FromResult(taxs.ToList());
    }

}
