
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

    public void Update(OvertimeUpdateDto overtimeUpdateDto,int id)
    {
        var overtime = _overtimeRepo.GetById(id);
        
        if (overtime.Result == null) return;
        if(overtimeUpdateDto.OtHours != null) overtime.Result.OtHours = overtimeUpdateDto.OtHours;
        if(overtimeUpdateDto.OtDate != null) overtime.Result.OtDate = overtimeUpdateDto.OtDate;
        if(overtimeUpdateDto.OtType != null) overtime.Result.OtType = overtimeUpdateDto.OtType;
        if(overtimeUpdateDto.Description != null) overtime.Result.Description = overtimeUpdateDto.Description;
        if(overtimeUpdateDto.Status != null) overtime.Result.Status = overtimeUpdateDto.Status;
        if(overtimeUpdateDto.ApprovedBy != null) overtime.Result.ApprovedBy = overtimeUpdateDto.ApprovedBy;
        if(overtimeUpdateDto.EmployeeId != null) overtime.Result.EmployeeId = overtimeUpdateDto.EmployeeId;

        _overtimeRepo.Update(overtime.Result);
    }

    public void Delete(int id)
    {
        var overtime = _overtimeRepo.GetById(id);
        if (overtime.Result != null) _overtimeRepo.Delete(overtime.Result);
    }

    public OvertimeReadDto? Get(int id)
    {
        var overtime = _overtimeRepo.GetById(id);
        if (overtime.Result == null) return null;
        return new OvertimeReadDto()
        {
            Id = overtime.Result.Id,
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
            Id = overtime.Id,
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
