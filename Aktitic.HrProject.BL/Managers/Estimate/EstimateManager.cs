using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EstimateManager:IEstimateManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EstimateManager( IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            CreatedAt = DateTime.Now
        }; 
        _unitOfWork.Estimate.Add(estimate);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(EstimateUpdateDto estimateUpdateDto, int id)
    {
        var estimate = _unitOfWork.Estimate.GetEstimateWithItems(id);
        if(estimateUpdateDto.Items != null)
        {
            var items = _unitOfWork.Item.GetItemsByEstimateId(id);
            foreach (var item in items)
            {
                _unitOfWork.Item.Delete(item);
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

        estimate.UpdatedAt = DateTime.Now;
        _unitOfWork.Estimate.Update(estimate);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var estimate = _unitOfWork.Estimate.GetById(id);
        if (estimate==null) return Task.FromResult(0);
        estimate.IsDeleted = true;
        estimate.DeletedAt = DateTime.Now;
        _unitOfWork.Estimate.Update(estimate);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<EstimateReadDto>? Get(int id)
    {
        var estimate = _unitOfWork.Estimate.GetEstimateWithItems(id);
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
            Client = _mapper.Map<Client,ClientDto>(estimate.Client) ,
            Project = new ProjectDto()
            {
                Id = estimate.Project?.Id,
                Name = estimate.Project?.Name,
                Description = estimate.Project?.Description,
                StartDate = estimate.Project?.StartDate,
                EndDate = estimate.Project?.EndDate,
                Status = estimate.Project?.Status,
                ClientId = estimate.Project?.ClientId,
                Client = _mapper.Map<Client,ClientDto>(estimate.Project?.Client),
                LeaderId = estimate.Project?.LeaderId,
                Leader = _mapper.Map<Employee,EmployeeDto>(estimate.Project?.Leader),
                TeamDto = (List<EmployeeDto>?)_mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>
                    (estimate.Project?.EmployeesProject?.Select(e => e.Employee))
                
            }
        });
    }

    public Task<List<EstimateReadDto>> GetAll()
    {
        var estimate = _unitOfWork.Estimate.GetAllEstimateWithItems();
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
        var estimates = await _unitOfWork.Estimate.GetAllEstimateWithItems();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = estimates.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var estimateList = estimates.ToList();

            var paginatedResults = estimateList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedEstimates = new List<EstimateDto>();
            foreach (var estimate in paginatedResults)
            {
                mappedEstimates.Add(new EstimateDto()
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
                    ClientId = estimate.Client?.FullName,
                    ProjectId = estimate.Project?.Name,
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

        if (estimates != null)
        {
            IEnumerable<Estimate> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(estimates, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(estimates, column, value2, operator2));
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
                    ClientId = estimate.Client?.FullName,
                    ProjectId = estimate.Project?.Name,
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
    private IEnumerable<Estimate> ApplyFilter(IEnumerable<Estimate> estimates, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => estimates.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => estimates.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => estimates.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => estimates.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var estimateValue) => ApplyNumericFilter(estimates, column, estimateValue, operatorType),
            _ => estimates
        };
    }

    private IEnumerable<Estimate> ApplyNumericFilter(IEnumerable<Estimate> estimates, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue == value),
        "neq" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue != value),
        "gte" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue >= value),
        "gt" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue > value),
        "lte" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue <= value),
        "lt" => estimates.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var estimateValue) && estimateValue < value),
        _ => estimates
    };
}


    public Task<List<EstimateDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Estimate> estimateDto;
            estimateDto = _unitOfWork.Estimate.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var estimate = _mapper.Map<IEnumerable<Estimate>, IEnumerable<EstimateDto>>(estimateDto);
            return Task.FromResult(estimate.ToList());
        }

        var  estimateDtos = _unitOfWork.Estimate.GlobalSearch(searchKey);
        var estimates = _mapper.Map<IEnumerable<Estimate>, IEnumerable<EstimateDto>>(estimateDtos);
        return Task.FromResult(estimates.ToList());
    }

}
