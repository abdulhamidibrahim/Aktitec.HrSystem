using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ShiftManager:IShiftManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ShiftManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public  Task<int> Add(ShiftAddDto shiftAddDto)
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
            Days = shiftAddDto.Days,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Shift.Add(shift);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ShiftUpdateDto shiftUpdateDto, int id)
    {
        var shift = _unitOfWork.Shift.GetById(id);

        if (shift == null) return Task.FromResult(0);
        if(shiftUpdateDto.Name != null) shift.Name = shiftUpdateDto.Name;
        if(shiftUpdateDto.MinStartTime != null) shift.MinStartTime = shiftUpdateDto.MinStartTime;
        if(shiftUpdateDto.MaxStartTime != null) shift.MaxStartTime = shiftUpdateDto.MaxStartTime;
        if(shiftUpdateDto.MinEndTime != null) shift.MinEndTime = shiftUpdateDto.MinEndTime;
        if(shiftUpdateDto.MaxEndTime != null) shift.MaxEndTime = shiftUpdateDto.MaxEndTime;
        if(shiftUpdateDto.BreakeTime != null) shift.BreakeTime = shiftUpdateDto.BreakeTime;
        if(shiftUpdateDto.EndDate != null) shift.EndDate = shiftUpdateDto.EndDate;
        if(shiftUpdateDto.RepeatEvery != null) shift.RepeatEvery = shiftUpdateDto.RepeatEvery;
        if(shiftUpdateDto.RecurringShift != null) shift.RecurringShift = shiftUpdateDto.RecurringShift;
        if(shiftUpdateDto.Indefinate != null) shift.Indefinate = shiftUpdateDto.Indefinate;
        if(shiftUpdateDto.Tag != null) shift.Tag = shiftUpdateDto.Tag;
        if(shiftUpdateDto.Note != null) shift.Note = shiftUpdateDto.Note;
        if(shiftUpdateDto.Status != null) shift.Status = shiftUpdateDto.Status;
        if(shiftUpdateDto.ApprovedBy != null) shift.ApprovedBy = shiftUpdateDto.ApprovedBy;
        if(shiftUpdateDto.StartTime != null) shift.StartTime = shiftUpdateDto.StartTime;
        if(shiftUpdateDto.EndDate != null) shift.EndTime = shiftUpdateDto.EndTime;
        if(shiftUpdateDto.Days != null) shift.Days = shiftUpdateDto.Days;

        shift.UpdatedAt = DateTime.Now;
        _unitOfWork.Shift.Update(shift);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var shift = _unitOfWork.Shift.GetById(id);
        if (shift==null) return Task.FromResult(0);
        shift.IsDeleted = true;
        shift.DeletedAt = DateTime.Now;
        _unitOfWork.Shift.Update(shift);
        return _unitOfWork.SaveChangesAsync();
    }

    public  ShiftReadDto? Get(int id)
    {
        var shift =  _unitOfWork.Shift.GetById(id);
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
        var shifts = await _unitOfWork.Shift.GetAll();
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
        var shifts = await _unitOfWork.Shift.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = shifts.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var shiftList = shifts.ToList();

            var paginatedResults = shiftList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(paginatedResults);
            FilteredShiftsDto result = new()
            {
                ShiftDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (shifts != null)
        {
            IEnumerable<Shift> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(shifts, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(shifts, column, value2, operator2));
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
    private IEnumerable<Shift> ApplyFilter(IEnumerable<Shift> shifts, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => shifts.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => shifts.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => shifts.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => shifts.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var shiftValue) => ApplyNumericFilter(shifts, column, shiftValue, operatorType),
            _ => shifts
        };
    }

    private IEnumerable<Shift> ApplyNumericFilter(IEnumerable<Shift> shifts, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue == value),
        "neq" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue != value),
        "gte" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue >= value),
        "gt" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue > value),
        "lte" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue <= value),
        "lt" => shifts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var shiftValue) && shiftValue < value),
        _ => shifts
    };
}


    public Task<List<ShiftDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Shift> shiftDto;
            shiftDto = _unitOfWork.Shift.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var shift = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(shiftDto);
            return Task.FromResult(shift.ToList());
        }

        var  shiftDtos = _unitOfWork.Shift.GlobalSearch(searchKey);
        var shifts = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDto>>(shiftDtos);
        return Task.FromResult(shifts.ToList());
    }

}
