using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PromotionManager:IPromotionManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PromotionManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(PromotionAddDto promotionAddDto)
    {
        var promotion = new Promotion()
        {
            EmployeeId = promotionAddDto.EmployeeId,
            PromotionFrom = promotionAddDto.PromotionFrom,
            PromotionToId = promotionAddDto.PromotionTo,
            Date = promotionAddDto.Date,
            CreatedAt = DateTime.Now,
        };
        

        _unitOfWork.Promotion.Add(promotion);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PromotionUpdateDto promotionUpdateDto, int id)
    {
        var promotion = _unitOfWork.Promotion.GetById(id);

        if (promotionUpdateDto.EmployeeId!=null) promotion.EmployeeId = promotionUpdateDto.EmployeeId;
        if (promotionUpdateDto.PromotionFrom!=null) promotion.PromotionFrom = promotionUpdateDto.PromotionFrom;
        if (promotionUpdateDto.PromotionTo != null) promotion.PromotionToId = promotionUpdateDto.PromotionTo;
        if (promotionUpdateDto.Date != null) promotion.Date = promotionUpdateDto.Date;
        if (promotion == null) return Task.FromResult(0);

        promotion.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Promotion.Update(promotion);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var promotion = _unitOfWork.Promotion.GetById(id);
        if (promotion==null) return Task.FromResult(0);
        promotion.IsDeleted = true;
        promotion.DeletedAt = DateTime.Now;
        _unitOfWork.Promotion.Update(promotion);
        return _unitOfWork.SaveChangesAsync();
    }

    public PromotionReadDto? Get(int id)
    {
        var promotion = _unitOfWork.Promotion.GetWithEmployees(id).FirstOrDefault();
        if (promotion == null) return null;
        
        return new PromotionReadDto()
        {
            Id = promotion.Id,
           PromotionFrom = promotion.PromotionFrom,
           Date = promotion.Date,
           PromotionTo = promotion.PromotionToId,
           EmployeeId = promotion.EmployeeId,
        };
    }

    public Task<List<PromotionReadDto>> GetAll()
    {
        var promotion = _unitOfWork.Promotion.GetAllWithEmployees();
        return Task.FromResult(promotion.Select(p => new PromotionReadDto()
        {
            Id = p.Id,
            PromotionFrom = p.PromotionFrom,
            Date = p.Date,
            PromotionTo = p.PromotionToId,
            EmployeeId = p.EmployeeId,
        }).ToList());
    }

   

    public async Task<FilteredPromotionsDto> GetFilteredPromotionAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var promotions =  _unitOfWork.Promotion.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = promotions.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var promotionList = promotions.ToList();

            var paginatedResults = promotionList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<PromotionDto>();
            foreach (var promotion in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new PromotionDto()
                {
                    Id = promotion.Id,
                    PromotionFrom = promotion.PromotionFrom,
                    Date = promotion.Date,
                    PromotionTo = _mapper.Map<Designation,DesignationDto>(promotion.PromotionTo),
                    EmployeeId = _mapper.Map<Employee,EmployeeDto>(promotion.Employee),
                });
            }
            FilteredPromotionsDto filteredBudgetsExpensesDto = new()
            {
                PromotionDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (promotions != null)
        {
            IEnumerable<Promotion> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(promotions, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(promotions, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var promotions = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(promotions);
            
            var mappedPromotion = new List<PromotionDto>();

            foreach (var promotion in paginatedResults)
            {
                
                mappedPromotion.Add(new PromotionDto()
                {
                    Id = promotion.Id,
                    PromotionFrom = promotion.PromotionFrom,
                    Date = promotion.Date,
                    PromotionTo = _mapper.Map<Designation,DesignationDto>(promotion.PromotionTo),
                    EmployeeId = _mapper.Map<Employee,EmployeeDto>(promotion.Employee),
                });
            }
            FilteredPromotionsDto filteredPromotionDto = new()
            {
                PromotionDto = mappedPromotion,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPromotionDto;
        }

        return new FilteredPromotionsDto();
    }
    private IEnumerable<Promotion> ApplyFilter(IEnumerable<Promotion> promotions, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => promotions.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => promotions.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => promotions.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => promotions.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var promotionValue) => ApplyNumericFilter(promotions, column, promotionValue, operatorType),
            _ => promotions
        };
    }

    private IEnumerable<Promotion> ApplyNumericFilter(IEnumerable<Promotion> promotions, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue == value),
        "neq" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue != value),
        "gte" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue >= value),
        "gt" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue > value),
        "lte" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue <= value),
        "lt" => promotions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var promotionValue) && promotionValue < value),
        _ => promotions
    };
}


    public Task<List<PromotionDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Promotion> promotionDto;
            promotionDto = _unitOfWork.Promotion.GetAllWithEmployees()
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            // map from promotion to promotionDto manually 
            
            var promotion = promotionDto.Select(p => new PromotionDto()
            {
                Id = p.Id,
                PromotionFrom = p.PromotionFrom,
                Date = p.Date,
                PromotionTo = _mapper.Map<Designation,DesignationDto>(p.PromotionTo),
                EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
            });    
            return Task.FromResult(promotion.ToList());
        }

        var  promotionDtos = _unitOfWork.Promotion.GlobalSearch(searchKey);
        var promotions = promotionDtos.Select(p => new PromotionDto()
        {
            Id = p.Id,
            PromotionFrom = p.PromotionFrom,
            Date = p.Date,
            PromotionTo = _mapper.Map<Designation,DesignationDto>(p.PromotionTo),
            EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
        });    
        return Task.FromResult(promotions.ToList());
    }

}
