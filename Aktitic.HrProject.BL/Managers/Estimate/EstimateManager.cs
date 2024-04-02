
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EstimateManager:IEstimateManager
{
    private readonly IEstimateRepo _estimateRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EstimateManager(IEstimateRepo estimateRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _estimateRepo = estimateRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
    }
    
    public Task<int> Add(EstimateAddDto estimateAddDto)
    {
        var estimate = new Estimate()
        {
            Email = estimateAddDto.Email,
            ClientAddress = estimateAddDto.ClientAddress,
            BillingAddress = estimateAddDto.BillingAddress,
            EstimateDate = estimateAddDto.EstimateDate,
            ExpiryDate = estimateAddDto.ExpiryDate,
            OtherInformation = estimateAddDto.OtherInformation,
            Status = estimateAddDto.Status,
            EstimateNumber = estimateAddDto.EstimateNumber,
            TotalAmount = estimateAddDto.TotalAmount,
            Discount = estimateAddDto.Discount,
            Tax = estimateAddDto.Tax,
            GrandTotal = estimateAddDto.GrandTotal,
            Items = _mapper.Map<IEnumerable<ItemDto>,IEnumerable<Item>>
                (estimateAddDto.Items ?? Array.Empty<ItemDto>()),
            ClientId = estimateAddDto.ClientId,
            ProjectId = estimateAddDto.ProjectId,
            
        }; 
        _estimateRepo.Add(estimate);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(EstimateUpdateDto estimateUpdateDto, int id)
    {
        var estimate = _estimateRepo.GetEstimateWithItems(id);
        if(estimateUpdateDto.Items != null)
        {
            var items = _itemRepo.GetItemsByEstimateId(id);
            foreach (var item in items)
            {
                _itemRepo.Delete(item);
            }

            if (estimate != null)
                estimate.Items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<Item>>(estimateUpdateDto.Items);
        }
        
        if (estimate == null) return Task.FromResult(0);
        
        if(estimateUpdateDto.Email != null) estimate.Email = estimateUpdateDto.Email;
        if(estimateUpdateDto.ClientAddress != null) estimate.ClientAddress = estimateUpdateDto.ClientAddress;
        if(estimateUpdateDto.BillingAddress != null) estimate.BillingAddress = estimateUpdateDto.BillingAddress;
        if(estimateUpdateDto.EstimateDate != null) estimate.EstimateDate = estimateUpdateDto.EstimateDate;
        if(estimateUpdateDto.ExpiryDate != null) estimate.ExpiryDate = estimateUpdateDto.ExpiryDate;
        if(estimateUpdateDto.Status != null) estimate.Status = estimateUpdateDto.Status;
        if(estimateUpdateDto.OtherInformation != null) estimate.OtherInformation = estimateUpdateDto.OtherInformation;
        if(estimateUpdateDto.EstimateNumber != null) estimate.EstimateNumber = estimateUpdateDto.EstimateNumber;
        if(estimateUpdateDto.TotalAmount != null) estimate.TotalAmount = estimateUpdateDto.TotalAmount;
        if(estimateUpdateDto.Discount != null) estimate.Discount = estimateUpdateDto.Discount;
        if(estimateUpdateDto.Tax != null) estimate.Tax = estimateUpdateDto.Tax;
        if(estimateUpdateDto.GrandTotal != null) estimate.GrandTotal = estimateUpdateDto.GrandTotal;
        if(estimateUpdateDto.ClientId != null) estimate.ClientId = estimateUpdateDto.ClientId;
        if(estimateUpdateDto.ProjectId != null) estimate.ProjectId = estimateUpdateDto.ProjectId;
        
        _estimateRepo.Update(estimate);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _estimateRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<EstimateReadDto>? Get(int id)
    {
        var estimate = _estimateRepo.GetEstimateWithItems(id);
        if (estimate == null) return null;
        return Task.FromResult(new EstimateReadDto()
        {
            Id = estimate.Id,
            Email = estimate.Email,
            ClientAddress = estimate.ClientAddress,
            BillingAddress = estimate.BillingAddress,
            EstimateDate = estimate.EstimateDate,
            ExpiryDate = estimate.ExpiryDate,
            OtherInformation = estimate.OtherInformation,
            Status = estimate.Status,
            EstimateNumber = estimate.EstimateNumber,
            TotalAmount = estimate.TotalAmount,
            Discount = estimate.Discount,
            Tax = estimate.Tax,
            GrandTotal = estimate.GrandTotal,
            Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(estimate.Items),
            ClientId = estimate.ClientId,
            ProjectId = estimate.ProjectId,
        });
    }

    public Task<List<EstimateReadDto>> GetAll()
    {
        var estimate = _estimateRepo.GetAllEstimateWithItems();
        return Task.FromResult(estimate.Result.Select(note => new EstimateReadDto()
        {
            Id = note.Id,
            Email = note.Email,
            ClientAddress = note.ClientAddress,
            BillingAddress = note.BillingAddress,
            EstimateDate = note.EstimateDate,
            ExpiryDate = note.ExpiryDate,
            OtherInformation = note.OtherInformation,
            Status = note.Status,
            EstimateNumber = note.EstimateNumber,
            TotalAmount = note.TotalAmount,
            Discount = note.Discount,
            Tax = note.Tax,
            GrandTotal = note.GrandTotal,
            Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(note.Items),
            ClientId = note.ClientId,
            ProjectId = note.ProjectId,
            
        }).ToList());
    }
    
     public async Task<FilteredEstimateDto> GetFilteredEstimatesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _estimateRepo.GetAllEstimateWithItems();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedEstimates = new List<EstimateDto>();
            foreach (var estimate in paginatedResults)
            {
                mappedEstimates.Add(new EstimateDto()
                {
                    Email = estimate.Email,
                    ClientAddress = estimate.ClientAddress,
                    BillingAddress = estimate.BillingAddress,
                    EstimateDate = estimate.EstimateDate,
                    ExpiryDate = estimate.ExpiryDate,
                    OtherInformation = estimate.OtherInformation,
                    Status = estimate.Status,
                    EstimateNumber = estimate.EstimateNumber,
                    TotalAmount = estimate.TotalAmount,
                    Discount = estimate.Discount,
                    Tax = estimate.Tax,
                    GrandTotal = estimate.GrandTotal,
                    Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(estimate.Items),
                    ClientId = estimate.ClientId,
                    ProjectId = estimate.ProjectId,
                });
            }
            FilteredEstimateDto filteredEstimateDto = new()
            {
                EstimateDto = mappedEstimates,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredEstimateDto;
        }

        if (users != null)
        {
            IEnumerable<Estimate> filteredResults;
        
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

            // var estimates = paginatedResults.ToList();
            // var mappedEstimate = _mapper.Map<IEnumerable<Estimate>, IEnumerable<EstimateDto>>(estimates);
            
            var mappedEstimates = new List<EstimateDto>();

            foreach (var estimate in paginatedResults)
            {
                
                mappedEstimates.Add(new EstimateDto()
                {
                    Email = estimate.Email,
                    ClientAddress = estimate.ClientAddress,
                    BillingAddress = estimate.BillingAddress,
                    EstimateDate = estimate.EstimateDate,
                    ExpiryDate = estimate.ExpiryDate,
                    OtherInformation = estimate.OtherInformation,
                    Status = estimate.Status,
                    EstimateNumber = estimate.EstimateNumber,
                    TotalAmount = estimate.TotalAmount,
                    Discount = estimate.Discount,
                    Tax = estimate.Tax,
                    GrandTotal = estimate.GrandTotal,
                    Items = _mapper.Map<IEnumerable<Item>,IEnumerable<ItemDto>>(estimate.Items),
                    ClientId = estimate.ClientId,
                    ProjectId = estimate.ProjectId,
                });
            }
            FilteredEstimateDto filteredEstimateDto = new()
            {
                EstimateDto = mappedEstimates,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredEstimateDto;
        }

        return new FilteredEstimateDto();
    }
    private IEnumerable<Estimate> ApplyFilter(IEnumerable<Estimate> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var estimateValue) => ApplyNumericFilter(users, column, estimateValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Estimate> ApplyNumericFilter(IEnumerable<Estimate> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue < value),
        _ => users
    };
}


    public Task<List<EstimateDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Estimate> user;
            user = _estimateRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var estimate = _mapper.Map<IEnumerable<Estimate>, IEnumerable<EstimateDto>>(user);
            return Task.FromResult(estimate.ToList());
        }

        var  users = _estimateRepo.GlobalSearch(searchKey);
        var estimates = _mapper.Map<IEnumerable<Estimate>, IEnumerable<EstimateDto>>(users);
        return Task.FromResult(estimates.ToList());
    }

}
