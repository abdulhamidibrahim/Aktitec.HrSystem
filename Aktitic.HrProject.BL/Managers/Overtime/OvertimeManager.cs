
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class OvertimeManager:IOvertimeManager
{
    private readonly IOvertimeRepo _overtimeRepo;

    public OvertimeManager(IOvertimeRepo overtimeRepo)
    {
        _overtimeRepo = overtimeRepo;
    }
    
    public void Add(OvertimeAddDto overtimeAddDto)
    {
        var overtime = new Overtime()
        {
            OtHours = overtimeAddDto.OtHours,
            OtDate = overtimeAddDto.OtDate,
            OtType = overtimeAddDto.OtType,
            Description = overtimeAddDto.Description,
            Status = overtimeAddDto.Status,
            ApprovedBy = overtimeAddDto.ApprovedBy,
            EmployeeId = overtimeAddDto.EmployeeId,
            
        };
        _overtimeRepo.Add(overtime);
    }

    public void Update(OvertimeUpdateDto overtimeUpdateDto)
    {
        var overtime = _overtimeRepo.GetById(overtimeUpdateDto.Id);
        
        if (overtime.Result == null) return;
        overtime.Result.OtHours = overtimeUpdateDto.OtHours;
        overtime.Result.OtDate = overtimeUpdateDto.OtDate;
        overtime.Result.OtType = overtimeUpdateDto.OtType;
        overtime.Result.Description = overtimeUpdateDto.Description;
        overtime.Result.Status = overtimeUpdateDto.Status;
        overtime.Result.ApprovedBy = overtimeUpdateDto.ApprovedBy;
        overtime.Result.EmployeeId = overtimeUpdateDto.EmployeeId;

        _overtimeRepo.Update(overtime.Result);
    }

    public void Delete(OvertimeDeleteDto overtimeDeleteDto)
    {
        var overtime = _overtimeRepo.GetById(overtimeDeleteDto.Id);
        if (overtime.Result != null) _overtimeRepo.Delete(overtime.Result);
    }

    public OvertimeReadDto? Get(int id)
    {
        var overtime = _overtimeRepo.GetById(id);
        if (overtime.Result == null) return null;
        return new OvertimeReadDto()
        {
            OtHours = overtime.Result.OtHours,
            OtDate = overtime.Result.OtDate,
            OtType = overtime.Result.OtType,
            Description = overtime.Result.Description,
            Status = overtime.Result.Status,
            ApprovedBy = overtime.Result.ApprovedBy,
            EmployeeId = overtime.Result.EmployeeId,
        };
    }

    public List<OvertimeReadDto> GetAll()
    {
        var overtimes = _overtimeRepo.GetAll();
        return overtimes.Result.Select(overtime => new OvertimeReadDto()
        {
            OtHours = overtime.OtHours,
            OtDate = overtime.OtDate,
            OtType = overtime.OtType,
            Description = overtime.Description,
            Status = overtime.Status,
            ApprovedBy = overtime.ApprovedBy,
            EmployeeId = overtime.EmployeeId,
            
        }).ToList();
    }
}
