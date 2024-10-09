using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class HolidayManager:IHolidayManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HolidayManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(HolidayAddDto holidayAddDto)
    {
        var holiday = new Holiday()
        {
            Title = holidayAddDto.Title,
            Date = holidayAddDto.Date,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Holiday.Add(holiday);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(HolidayUpdateDto holidayUpdateDto, int id)
    {
        var holiday = _unitOfWork.Holiday.GetById(id);

        if (holiday == null) return Task.FromResult(0);
        if(holidayUpdateDto.Title != null) holiday.Title = holidayUpdateDto.Title;
        if(holidayUpdateDto.Date != null) holiday.Date = holidayUpdateDto.Date;

        holiday.UpdatedAt = DateTime.Now;
        _unitOfWork.Holiday.Update(holiday);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var holiday = _unitOfWork.Holiday.GetById(id);
        if (holiday==null) return Task.FromResult(0);
        holiday.IsDeleted = true;
        holiday.DeletedAt = DateTime.Now;
        _unitOfWork.Holiday.Update(holiday);
        return _unitOfWork.SaveChangesAsync();
    }

    public HolidayReadDto? Get(int id)
    {
        var holiday = _unitOfWork.Holiday.GetById(id);
        if (holiday == null) return null;
        return new HolidayReadDto()
        {
            Id = holiday.Id,
            Title = holiday.Title,
            Date = holiday.Date
        };
    }

    public List<HolidayReadDto> GetAll()
    {
        var holidays = _unitOfWork.Holiday.GetAll();
        return holidays.Result.Select(holiday => new HolidayReadDto()
        {
            Id = holiday.Id,
            Title = holiday.Title,
            Date = holiday.Date
        }).ToList();
    }
    
     public async Task<FilteredHolidayDto> GetFilteredHolidaysAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var holidays = await _unitOfWork.Holiday.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = holidays.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var holidayList = holidays.ToList();

            var paginatedResults = holidayList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(paginatedResults);
            FilteredHolidayDto result = new()
            {
                HolidayDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (holidays != null)
        {
            IEnumerable<Holiday> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(holidays, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(holidays, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedHoliday = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(paginatedResults);

            FilteredHolidayDto filteredHolidayDto = new()
            {
                HolidayDto = mappedHoliday,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredHolidayDto;
        }

        return new FilteredHolidayDto();
    }
    private IEnumerable<Holiday> ApplyFilter(IEnumerable<Holiday> holidays, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => holidays.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => holidays.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => holidays.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => holidays.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var holidayValue) => ApplyNumericFilter(holidays, column, holidayValue, operatorType),
            _ => holidays
        };
    }

    private IEnumerable<Holiday> ApplyNumericFilter(IEnumerable<Holiday> holidays, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue == value),
        "neq" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue != value),
        "gte" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue >= value),
        "gt" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue > value),
        "lte" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue <= value),
        "lt" => holidays.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue < value),
        _ => holidays
    };
}


    public Task<List<HolidayDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Holiday> holidayDto;
            holidayDto = _unitOfWork.Holiday.GetAll().Result.Where(e => e.GetPropertyValue(column).Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var holiday = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(holidayDto);
            return Task.FromResult(holiday.ToList());
        }

        var  holidaysDto = _unitOfWork.Holiday.GlobalSearch(searchKey);
        var holidays = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(holidaysDto);
        return Task.FromResult(holidays.ToList());
    }

}
