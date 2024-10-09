using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class LeaveSettingsManager:ILeaveSettingsManager
{

    private readonly IUnitOfWork _unitOfWork;
    public LeaveSettingsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(LeaveSettingAddDto leaveSettingAddDto)
    { 
        
        var leaveSetting = new LeaveSettings
        {
            AnnualDays = leaveSettingAddDto.Annual!.Days,
            AnnualEarnedLeave = leaveSettingAddDto.Annual.EarnedLeave,
            AnnualCarryForward = leaveSettingAddDto.Annual.CarryForward,
            AnnualCarryForwardMax = leaveSettingAddDto.Annual.CarryForwardMax,
            AnnualActive = leaveSettingAddDto.Annual.Active,
            LopDays = leaveSettingAddDto.Lop!.Days,
            LopCarryForward = leaveSettingAddDto.Lop.CarryForward,
            LopCarryForwardMax = leaveSettingAddDto.Lop.CarryForwardMax,
            LopEarnedLeave = leaveSettingAddDto.Lop.EarnedLeave,
            LopActive = leaveSettingAddDto.Lop.Active,
            MaternityDays = leaveSettingAddDto.Maternity!.Days,
            MaternityActive = leaveSettingAddDto.Maternity.Active,
            PaternityDays = leaveSettingAddDto.Paternity!.Days,
            PaternityActive = leaveSettingAddDto.Paternity.Active,
            SickDays = leaveSettingAddDto.Sick!.Days,
            SickActive = leaveSettingAddDto.Sick.Active,
            HospitalisationDays = leaveSettingAddDto.Hospitalisation!.Days,
            HospitalisationActive = leaveSettingAddDto.Hospitalisation.Active,
            CreatedAt = DateTime.Now,
        };

         _unitOfWork.LeaveSettings.Add(leaveSetting);
         return _unitOfWork.SaveChangesAsync();

    }
public Task<int> Update(LeaveSettingUpdateDto leaveSettingUpdateDto, int id)
{
    try
    {
        // Retrieve the leave setting from the repository
        var leaveSetting = _unitOfWork.LeaveSettings.GetById(id);

        if (leaveSetting != null)
        {
            // LogNote leave setting properties based on DTO
            if (leaveSettingUpdateDto?.Annual != null)
            {
                leaveSetting.AnnualDays = leaveSettingUpdateDto.Annual.Days;
                leaveSetting.AnnualEarnedLeave = leaveSettingUpdateDto.Annual.EarnedLeave;
                leaveSetting.AnnualCarryForward = leaveSettingUpdateDto.Annual.CarryForward;
                leaveSetting.AnnualCarryForwardMax = leaveSettingUpdateDto.Annual.CarryForwardMax;
                leaveSetting.AnnualActive = leaveSettingUpdateDto.Annual.Active;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            if (leaveSettingUpdateDto?.Lop != null)
            {
                leaveSetting.LopDays = leaveSettingUpdateDto.Lop.Days;
                leaveSetting.LopActive = leaveSettingUpdateDto.Lop.Active;
                leaveSetting.LopCarryForward = leaveSettingUpdateDto.Lop.CarryForward;
                leaveSetting.LopCarryForwardMax = leaveSettingUpdateDto.Lop.CarryForwardMax;
                leaveSetting.LopEarnedLeave = leaveSettingUpdateDto.Lop.EarnedLeave;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            if (leaveSettingUpdateDto?.Maternity != null)
            {
                leaveSetting.MaternityActive = leaveSettingUpdateDto.Maternity.Active;
                leaveSetting.MaternityDays = leaveSettingUpdateDto.Maternity.Days;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            if (leaveSettingUpdateDto?.Paternity != null)
            {
                leaveSetting.PaternityActive = leaveSettingUpdateDto.Paternity.Active;
                leaveSetting.PaternityDays = leaveSettingUpdateDto.Paternity.Days;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            if (leaveSettingUpdateDto?.Sick != null)
            {
                leaveSetting.SickActive = leaveSettingUpdateDto.Sick.Active;
                leaveSetting.SickDays = leaveSettingUpdateDto.Sick.Days;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            if (leaveSettingUpdateDto?.Hospitalisation != null)
            {
                leaveSetting.HospitalisationActive = leaveSettingUpdateDto.Hospitalisation.Active;
                leaveSetting.HospitalisationDays = leaveSettingUpdateDto.Hospitalisation.Days;
                leaveSetting.UpdatedAt = DateTime.Now;
            }

            // Save changes to the repository
            
             _unitOfWork.LeaveSettings.Update(leaveSetting);
             return _unitOfWork.SaveChangesAsync();
        }
        
    }
    catch (Exception ex)
    {
        // Log the error and return error code
        Console.WriteLine($"Error updating leave setting: {ex.Message}");
        return Task.FromResult(0);
    }

    return Task.FromResult(0);
}



public Task<int> Delete(int id)
{
    var leaves = _unitOfWork.LeaveSettings.GetById(id);
    if (leaves==null) return Task.FromResult(0);
    leaves.IsDeleted = true;
    leaves.DeletedAt = DateTime.Now;
    _unitOfWork.LeaveSettings.Update(leaves);
    return _unitOfWork.SaveChangesAsync();
}

public LeaveSettingReadDto? Get(int id)
{
    var leaveSetting = _unitOfWork.LeaveSettings.GetById(id);
    if (leaveSetting == null) return null;

    var annual = new AnnualDto()
    {
        Active = leaveSetting.AnnualActive,
        CarryForward = leaveSetting.AnnualCarryForward,
        CarryForwardMax = leaveSetting.AnnualCarryForwardMax,
        Days = leaveSetting.AnnualDays,
        EarnedLeave = leaveSetting.AnnualEarnedLeave
    };
    
    var maternity = new MaternityDto()
    {
        Active = leaveSetting.MaternityActive,
        Days = leaveSetting.MaternityDays
    };
    
    var paternity = new PaternityDto()
    {
        Active = leaveSetting.PaternityActive,
        Days = leaveSetting.PaternityDays
    };
    var hospitalisation = new HospitalisationDto()
    {
        Active = leaveSetting.HospitalisationActive,
        Days = leaveSetting.HospitalisationDays
    };
    
    var lop = new LopDto()
    {
        Active = leaveSetting.LopActive,
        CarryForward = leaveSetting.LopCarryForward,
        CarryForwardMax = leaveSetting.LopCarryForwardMax,
        Days = leaveSetting.LopDays,
        EarnedLeave = leaveSetting.LopEarnedLeave
    };
    var sick = new SickDto()
    {
        Active = leaveSetting.SickActive,
        Days = leaveSetting.SickDays
    };

    var leaveSettingReadDto = new LeaveSettingReadDto()
    {
        Id = leaveSetting.Id,
        Annual = annual,
        Maternity = maternity,
        Hospitalisation = hospitalisation,
        Lop = lop,
        Paternity = paternity,
        Sick = sick
    };

    return leaveSettingReadDto;

}

public List<LeaveSettingReadDto> GetAll()
{
    var leaveSettings = _unitOfWork.LeaveSettings.GetAll();
    return leaveSettings.Result.Select(leaveSetting => new LeaveSettingReadDto()
    {
        Id = leaveSetting.Id,
        Annual = new AnnualDto()
        {
            Active = leaveSetting.AnnualActive,
            CarryForward = leaveSetting.AnnualCarryForward,
            CarryForwardMax = leaveSetting.AnnualCarryForwardMax,
            Days = leaveSetting.AnnualDays,
            EarnedLeave = leaveSetting.AnnualEarnedLeave
        },
        Maternity = new MaternityDto()
        {
            Active = leaveSetting.MaternityActive,
            Days = leaveSetting.MaternityDays
        },
        Paternity = new PaternityDto()
        {
            Active = leaveSetting.PaternityActive,
            Days = leaveSetting.PaternityDays
        },
        Hospitalisation = new HospitalisationDto()
        {
            Active = leaveSetting.HospitalisationActive,
            Days = leaveSetting.HospitalisationDays
        },
        Lop = new LopDto()
        {
            Active = leaveSetting.LopActive,
            CarryForward = leaveSetting.LopCarryForward,
            CarryForwardMax = leaveSetting.LopCarryForwardMax,
            Days = leaveSetting.LopDays,
            EarnedLeave = leaveSetting.LopEarnedLeave
        },
        Sick = new SickDto()
        {
            Active = leaveSetting.SickActive,
            Days = leaveSetting.SickDays
        }
    }).ToList(); 
    }
}
