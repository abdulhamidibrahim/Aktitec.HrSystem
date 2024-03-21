
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ShiftManager:IShiftManager
{
    private readonly IShiftRepo _shiftRepo;
    private readonly IMapper _mapper;

    public ShiftManager(IShiftRepo shiftRepo, IMapper mapper)
    {
        _shiftRepo = shiftRepo;
        _mapper = mapper;
    }
    
    public async Task<int> Add(ShiftAddDto shiftAddDto)
    {
        var shift = new Shift()
        {
            Name = shiftAddDto.Name,
            MinStartTime = shiftAddDto.MinStartTime ,
            MaxStartTime =shiftAddDto.MaxStartTime,
            MinEndTime = shiftAddDto.MinEndTime,
            MaxEndTime = shiftAddDto.MaxEndTime,
            BreakeTime = shiftAddDto.BreakeTime,
            EndDate = shiftAddDto.EndDate,
            RepeatEvery = shiftAddDto.RepeatEvery,
            RecurringShift = shiftAddDto.RecurringShift,
            Indefinate = shiftAddDto.Indefinate,
            Tag = shiftAddDto.Tag,
            Note = shiftAddDto.Note,
            Status = shiftAddDto.Status,
            ApprovedBy = shiftAddDto.ApprovedBy,
            StartTime = shiftAddDto.StartTime,
            EndTime = shiftAddDto.EndTime,
            Days = shiftAddDto.Days
        };
        return await _shiftRepo.Add(shift);
    }

    public async Task<int> Update(ShiftUpdateDto shiftUpdateDto,int id)
    {
        var shift = _shiftRepo.GetById(id);

        if (shift.Result == null) return 0;
        if(shiftUpdateDto.Name != null) shift.Result.Name = shiftUpdateDto.Name;
        if(shiftUpdateDto.MinStartTime != null) shift.Result.MinStartTime = shiftUpdateDto.MinStartTime;
        if(shiftUpdateDto.MaxStartTime != null) shift.Result.MaxStartTime = shiftUpdateDto.MaxStartTime;
        if(shiftUpdateDto.MinEndTime != null) shift.Result.MinEndTime = shiftUpdateDto.MinEndTime;
        if(shiftUpdateDto.MaxEndTime != null) shift.Result.MaxEndTime = shiftUpdateDto.MaxEndTime;
        if(shiftUpdateDto.BreakeTime != null) shift.Result.BreakeTime = shiftUpdateDto.BreakeTime;
        if(shiftUpdateDto.EndDate != null) shift.Result.EndDate = shiftUpdateDto.EndDate;
        if(shiftUpdateDto.RepeatEvery != null) shift.Result.RepeatEvery = shiftUpdateDto.RepeatEvery;
        if(shiftUpdateDto.RecurringShift != null) shift.Result.RecurringShift = shiftUpdateDto.RecurringShift;
        if(shiftUpdateDto.Indefinate != null) shift.Result.Indefinate = shiftUpdateDto.Indefinate;
        if(shiftUpdateDto.Tag != null) shift.Result.Tag = shiftUpdateDto.Tag;
        if(shiftUpdateDto.Note != null) shift.Result.Note = shiftUpdateDto.Note;
        if(shiftUpdateDto.Status != null) shift.Result.Status = shiftUpdateDto.Status;
        if(shiftUpdateDto.ApprovedBy != null) shift.Result.ApprovedBy = shiftUpdateDto.ApprovedBy;
        if(shiftUpdateDto.StartTime != null) shift.Result.StartTime = shiftUpdateDto.StartTime;
        if(shiftUpdateDto.EndDate != null) shift.Result.EndTime = shiftUpdateDto.EndTime;
        if(shiftUpdateDto.Days != null) shift.Result.Days = shiftUpdateDto.Days;
        
       return await _shiftRepo.Update(shift.Result);
    }

    public Task<int> Delete(int id)
    {
        var shift = _shiftRepo.GetById(id);
        if (shift.Result != null) return _shiftRepo.Delete(shift.Result);
        return Task.FromResult(0);
    }

    public async Task<ShiftReadDto?> Get(int id)
    {
        var shift = await _shiftRepo.GetById(id);
        if (shift == null) return null;
        return new ShiftReadDto()
        {
            Id = shift.Id,
            Name = shift.Name,
            MinStartTime = shift.MinStartTime,
            MaxStartTime = shift.MaxStartTime,
            MinEndTime = shift.MinEndTime,
            MaxEndTime = shift.MaxEndTime,
            BreakeTime = shift.BreakeTime,
            EndDate = shift.EndDate,
            RepeatEvery = shift.RepeatEvery,
            RecurringShift = shift.RecurringShift,
            Indefinate = shift.Indefinate,
            Tag = shift.Tag,
            Note = shift.Note,
            Status = shift.Status,
            ApprovedBy = shift.ApprovedBy,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Days = shift.Days,
        };
    }

    public async Task<List<ShiftReadDto>> GetAll()
    {
        var shifts = await _shiftRepo.GetAll();
        return shifts.Select(shift => new ShiftReadDto()
        {
            Id = shift.Id,
            Name = shift.Name,
            MinStartTime = shift.MinStartTime,
            MaxStartTime = shift.MaxStartTime,
            MinEndTime = shift.MinEndTime,
            MaxEndTime = shift.MaxEndTime,
            BreakeTime = shift.BreakeTime,
            EndDate = shift.EndDate,
            RepeatEvery = shift.RepeatEvery,
            RecurringShift = shift.RecurringShift,
            Indefinate = shift.Indefinate,
            Tag = shift.Tag,
            Note = shift.Note,
            Status = shift.Status,
            ApprovedBy = shift.ApprovedBy,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Days = shift.Days
        }).ToList();
    }
    
      public async Task<FilteredShiftsDto> GetFilteredShiftsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _shiftRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(paginatedResults);
            FilteredShiftsDto result = new()
            {
                ShiftDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Shift> filteredResults;
        
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

            var mappedShift = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(paginatedResults);

            FilteredShiftsDto filteredShiftDto = new()
            {
                ShiftDto = mappedShift,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredShiftDto;
        }

        return new FilteredShiftsDto();
    }
    private IEnumerable<Shift> ApplyFilter(IEnumerable<Shift> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var shiftValue) => ApplyNumericFilter(users, column, shiftValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Shift> ApplyNumericFilter(IEnumerable<Shift> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue < value),
        _ => users
    };
}


    public Task<List<ShiftDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Shift> user;
            user = _shiftRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var shift = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(user);
            return Task.FromResult(shift.ToList());
        }

        var  users = _shiftRepo.GlobalSearch(searchKey);
        var shifts = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(users);
        return Task.FromResult(shifts.ToList());
    }

}
