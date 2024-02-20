
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class LeavesManager:ILeavesManager
{
    private readonly ILeavesRepo _leavesRepo;

    public LeavesManager(ILeavesRepo leavesRepo)
    {
        _leavesRepo = leavesRepo;
    }
    
    public void Add(LeavesAddDto leavesAddDto)
    {
        var leaves = new Leaves()
        {
            EmployeeId = leavesAddDto.EmployeeId,
            Type = leavesAddDto.Type,
            FromDate = leavesAddDto.FromDate,
            ToDate = leavesAddDto.ToDate,
            Reason = leavesAddDto.Reason,
            ApprovedBy = leavesAddDto.ApprovedBy,
            Days = leavesAddDto.Days,
            Approved = leavesAddDto.Approved,
            
            
        };
        _leavesRepo.Add(leaves);
    }

    public void Update(LeavesUpdateDto leavesUpdateDto)
    {
        var leaves = _leavesRepo.GetById(leavesUpdateDto.Id);
        
        if (leaves.Result == null) return;
        leaves.Result.EmployeeId = leavesUpdateDto.EmployeeId;
        leaves.Result.Type = leavesUpdateDto.Type;
        leaves.Result.FromDate = leavesUpdateDto.FromDate;
        leaves.Result.ToDate = leavesUpdateDto.ToDate;
        leaves.Result.Reason = leavesUpdateDto.Reason;
        leaves.Result.ApprovedBy = leavesUpdateDto.ApprovedBy;
        leaves.Result.Days = leavesUpdateDto.Days;
        leaves.Result.Approved = leavesUpdateDto.Approved;
        
        _leavesRepo.Update(leaves.Result);
    }

    public void Delete(LeavesDeleteDto leavesDeleteDto)
    {
        var leaves = _leavesRepo.GetById(leavesDeleteDto.Id);
        if (leaves.Result != null) _leavesRepo.Delete(leaves.Result);
    }

    public LeavesReadDto? Get(int id)
    {
        var leaves = _leavesRepo.GetById(id);
        if (leaves.Result == null) return null;
        return new LeavesReadDto()
        {
            EmployeeId = leaves.Result.EmployeeId,
            Type = leaves.Result.Type,
            FromDate = leaves.Result.FromDate,
            ToDate = leaves.Result.ToDate,
            Reason = leaves.Result.Reason,
            ApprovedBy = leaves.Result.ApprovedBy,
            Days = leaves.Result.Days,
            Approved = leaves.Result.Approved,
            
        };
    }

    public List<LeavesReadDto> GetAll()
    {
        var leaves = _leavesRepo.GetAll();
        return leaves.Result.Select(leave => new LeavesReadDto()
        {
            EmployeeId = leave.EmployeeId,
            Type = leave.Type,
            FromDate = leave.FromDate,
            ToDate = leave.ToDate,
            Reason = leave.Reason,
            ApprovedBy = leave.ApprovedBy,
            Days = leave.Days,
            Approved = leave.Approved,
            
            
        }).ToList();
    }
}
