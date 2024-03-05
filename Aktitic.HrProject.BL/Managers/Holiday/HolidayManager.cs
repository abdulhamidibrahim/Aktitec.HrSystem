
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class HolidayManager:IHolidayManager
{
    private readonly IHolidayRepo _holidayRepo;

    public HolidayManager(IHolidayRepo holidayRepo)
    {
        _holidayRepo = holidayRepo;
    }
    
    public void Add(HolidayAddDto holidayAddDto)
    {
        var holiday = new Holiday()
        {
            Title = holidayAddDto.Title,
            Date = holidayAddDto.Date
        };
        _holidayRepo.Add(holiday);
    }

    public void Update(HolidayUpdateDto holidayUpdateDto)
    {
        var holiday = _holidayRepo.GetById(holidayUpdateDto.Id);
        
        if (holiday.Result == null) return;
        holiday.Result.Title = holidayUpdateDto.Title;
        holiday.Result.Date = holidayUpdateDto.Date;


        _holidayRepo.Update(holiday.Result);
    }

    public void Delete(HolidayDeleteDto holidayDeleteDto)
    {
        var holiday = _holidayRepo.GetById(holidayDeleteDto.Id);
        if (holiday.Result != null) _holidayRepo.Delete(holiday.Result);
    }

    public HolidayReadDto? Get(int id)
    {
        var holiday = _holidayRepo.GetById(id);
        if (holiday.Result == null) return null;
        return new HolidayReadDto()
        {
            Id = holiday.Result.Id,
            Title = holiday.Result.Title,
            Date = holiday.Result.Date
        };
    }

    public List<HolidayReadDto> GetAll()
    {
        var holidays = _holidayRepo.GetAll();
        return holidays.Result.Select(holiday => new HolidayReadDto()
        {
            Title = holiday.Title,
            Date = holiday.Date
        }).ToList();
    }
}
