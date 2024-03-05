
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class ShiftManager:IShiftManager
{
    private readonly IShiftRepo _shiftRepo;

    public ShiftManager(IShiftRepo shiftRepo)
    {
        _shiftRepo = shiftRepo;
    }
    
    public void Add(ShiftAddDto shiftAddDto)
    {
        var shift = new Shift()
        {
            Name = shiftAddDto.Name,
            MinStartTime = shiftAddDto.MinStartTime,
            MaxStartTime = shiftAddDto.MaxStartTime,
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
            
        };
        _shiftRepo.Add(shift);
    }

    public void Update(ShiftUpdateDto shiftUpdateDto)
    {
        var shift = _shiftRepo.GetById(shiftUpdateDto.Id);
        
        if (shift.Result == null) return;
        shift.Result.Name = shiftUpdateDto.Name;
        shift.Result.MinStartTime = shiftUpdateDto.MinStartTime;
        shift.Result.MaxStartTime = shiftUpdateDto.MaxStartTime;
        shift.Result.MinEndTime = shiftUpdateDto.MinEndTime;
        shift.Result.MaxEndTime = shiftUpdateDto.MaxEndTime;
        shift.Result.BreakeTime = shiftUpdateDto.BreakeTime;
        shift.Result.EndDate = shiftUpdateDto.EndDate;
        shift.Result.RepeatEvery = shiftUpdateDto.RepeatEvery;
        shift.Result.RecurringShift = shiftUpdateDto.RecurringShift;
        shift.Result.Indefinate = shiftUpdateDto.Indefinate;
        shift.Result.Tag = shiftUpdateDto.Tag;
        shift.Result.Note = shiftUpdateDto.Note;
        shift.Result.Status = shiftUpdateDto.Status;
        shift.Result.ApprovedBy = shiftUpdateDto.ApprovedBy;
        shift.Result.StartTime = shiftUpdateDto.StartTime;
        shift.Result.EndTime = shiftUpdateDto.EndTime;
        


        _shiftRepo.Update(shift.Result);
    }

    public void Delete(ShiftDeleteDto shiftDeleteDto)
    {
        var shift = _shiftRepo.GetById(shiftDeleteDto.Id);
        if (shift.Result != null) _shiftRepo.Delete(shift.Result);
    }

    public ShiftReadDto? Get(int id)
    {
        var shift = _shiftRepo.GetById(id);
        if (shift.Result == null) return null;
        return new ShiftReadDto()
        {
            Id = shift.Result.Id,
            Name = shift.Result.Name,
            MinStartTime = shift.Result.MinStartTime,
            MaxStartTime = shift.Result.MaxStartTime,
            MinEndTime = shift.Result.MinEndTime,
            MaxEndTime = shift.Result.MaxEndTime,
            BreakeTime = shift.Result.BreakeTime,
            EndDate = shift.Result.EndDate,
            RepeatEvery = shift.Result.RepeatEvery,
            RecurringShift = shift.Result.RecurringShift,
            Indefinate = shift.Result.Indefinate,
            Tag = shift.Result.Tag,
            Note = shift.Result.Note,
            Status = shift.Result.Status,
            ApprovedBy = shift.Result.ApprovedBy,
            StartTime = shift.Result.StartTime,
            EndTime = shift.Result.EndTime,
            
        };
    }

    public List<ShiftReadDto> GetAll()
    {
        var shifts = _shiftRepo.GetAll();
        return shifts.Result.Select(shift => new ShiftReadDto()
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
            
        }).ToList();
    }
}
