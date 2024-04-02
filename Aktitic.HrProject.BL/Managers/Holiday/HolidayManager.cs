
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class HolidayManager:IHolidayManager
{
    private readonly IHolidayRepo _holidayRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HolidayManager(IHolidayRepo holidayRepo, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _holidayRepo = holidayRepo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(HolidayAddDto holidayAddDto)
    {
        var holiday = new Holiday()
        {
            Title = holidayAddDto.Title,
            Date = holidayAddDto.Date
        }; 
        _holidayRepo.Add(holiday);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(HolidayUpdateDto holidayUpdateDto, int id)
    {
        var holiday = _holidayRepo.GetById(id);

        if (holiday == null) return Task.FromResult(0);
        if(holidayUpdateDto.Title != null) holiday.Title = holidayUpdateDto.Title;
        if(holidayUpdateDto.Date != null) holiday.Date = holidayUpdateDto.Date;


        _holidayRepo.Update(holiday);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _holidayRepo.GetById(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public HolidayReadDto? Get(int id)
    {
        var holiday = _holidayRepo.GetById(id);
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
        var holidays = _holidayRepo.GetAll();
        return holidays.Result.Select(holiday => new HolidayReadDto()
        {
            Id = holiday.Id,
            Title = holiday.Title,
            Date = holiday.Date
        }).ToList();
    }
    
     public async Task<FilteredHolidayDto> GetFilteredHolidaysAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _holidayRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(paginatedResults);
            FilteredHolidayDto result = new()
            {
                HolidayDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Holiday> filteredResults;
        
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
    private IEnumerable<Holiday> ApplyFilter(IEnumerable<Holiday> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var holidayValue) => ApplyNumericFilter(users, column, holidayValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Holiday> ApplyNumericFilter(IEnumerable<Holiday> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var holidayValue) && holidayValue < value),
        _ => users
    };
}


    public Task<List<HolidayDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Holiday> user;
            user = _holidayRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var holiday = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(user);
            return Task.FromResult(holiday.ToList());
        }

        var  users = _holidayRepo.GlobalSearch(searchKey);
        var holidays = _mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDto>>(users);
        return Task.FromResult(holidays.ToList());
    }

}
