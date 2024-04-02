
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class LeaveSettingsManager:ILeaveSettingsManager
{

    private readonly ILeaveSettingRepo _leaveSettingRepo;
    private readonly IUnitOfWork _unitOfWork;
    public LeaveSettingsManager(ILeaveSettingRepo leaveSettingRepo, IUnitOfWork unitOfWork)
    {
        _leaveSettingRepo = leaveSettingRepo;
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
            HospitalisationActive = leaveSettingAddDto.Hospitalisation.Active
        };

         _leaveSettingRepo.Add(leaveSetting);
         return _unitOfWork.SaveChangesAsync();

    }
public Task<int> Update(LeaveSettingUpdateDto leaveSettingUpdateDto, int id)
{
    try
    {
        // Retrieve the leave setting from the repository
        var leaveSetting = _leaveSettingRepo.GetById(id);

        if (leaveSetting != null)
        {
            // Update leave setting properties based on DTO
            if (leaveSettingUpdateDto?.Annual != null)
            {
                leaveSetting.AnnualDays = leaveSettingUpdateDto.Annual.Days;
                leaveSetting.AnnualEarnedLeave = leaveSettingUpdateDto.Annual.EarnedLeave;
                leaveSetting.AnnualCarryForward = leaveSettingUpdateDto.Annual.CarryForward;
                leaveSetting.AnnualCarryForwardMax = leaveSettingUpdateDto.Annual.CarryForwardMax;
                leaveSetting.AnnualActive = leaveSettingUpdateDto.Annual.Active;
            }

            if (leaveSettingUpdateDto?.Lop != null)
            {
                leaveSetting.LopDays = leaveSettingUpdateDto.Lop.Days;
                leaveSetting.LopActive = leaveSettingUpdateDto.Lop.Active;
                leaveSetting.LopCarryForward = leaveSettingUpdateDto.Lop.CarryForward;
                leaveSetting.LopCarryForwardMax = leaveSettingUpdateDto.Lop.CarryForwardMax;
                leaveSetting.LopEarnedLeave = leaveSettingUpdateDto.Lop.EarnedLeave;
            }

            if (leaveSettingUpdateDto?.Maternity != null)
            {
                leaveSetting.MaternityActive = leaveSettingUpdateDto.Maternity.Active;
                leaveSetting.MaternityDays = leaveSettingUpdateDto.Maternity.Days;
            }

            if (leaveSettingUpdateDto?.Paternity != null)
            {
                leaveSetting.PaternityActive = leaveSettingUpdateDto.Paternity.Active;
                leaveSetting.PaternityDays = leaveSettingUpdateDto.Paternity.Days;
            }

            if (leaveSettingUpdateDto?.Sick != null)
            {
                leaveSetting.SickActive = leaveSettingUpdateDto.Sick.Active;
                leaveSetting.SickDays = leaveSettingUpdateDto.Sick.Days;
            }

            if (leaveSettingUpdateDto?.Hospitalisation != null)
            {
                leaveSetting.HospitalisationActive = leaveSettingUpdateDto.Hospitalisation.Active;
                leaveSetting.HospitalisationDays = leaveSettingUpdateDto.Hospitalisation.Days;
            }

            // Save changes to the repository
             _leaveSettingRepo.Update(leaveSetting);
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

    _leaveSettingRepo.GetById(id);
    return _unitOfWork.SaveChangesAsync();
}

public LeaveSettingReadDto? Get(int id)
{
    var leaveSetting = _leaveSettingRepo.GetById(id);
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
    var leaveSettings = _leaveSettingRepo.GetAll();
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
