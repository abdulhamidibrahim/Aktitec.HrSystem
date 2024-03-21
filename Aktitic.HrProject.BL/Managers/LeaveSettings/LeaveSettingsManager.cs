
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class LeaveSettingsManager:ILeaveSettingsManager
{
    private readonly ILeaveSettingRepo _leaveSettingRepo;

    public LeaveSettingsManager(ILeaveSettingRepo leaveSettingRepo)
    {
        _leaveSettingRepo = leaveSettingRepo;
       
    }
    
    public void Add(LeaveSettingAddDto leaveSettingAddDto)
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

    }

    public void Update(LeaveSettingUpdateDto leaveSettingUpdateDto, int id)
    {
        var leaveSetting = _leaveSettingRepo.GetById(id).Result;
       if (leaveSetting != null) return;
       if(leaveSettingUpdateDto?.Annual?.Days != null) leaveSetting!.AnnualDays = leaveSettingUpdateDto.Annual!.Days;
       if(leaveSettingUpdateDto?.Annual?.EarnedLeave != null) leaveSetting!.AnnualEarnedLeave = leaveSettingUpdateDto.Annual.EarnedLeave;
       if(leaveSettingUpdateDto?.Annual?.CarryForward != null) leaveSetting!.AnnualCarryForward = leaveSettingUpdateDto.Annual.CarryForward;
       if(leaveSettingUpdateDto?.Annual?.CarryForwardMax != null) leaveSetting!.AnnualCarryForwardMax = leaveSettingUpdateDto.Annual.CarryForwardMax;
       if(leaveSettingUpdateDto?.Annual?.Active != null) leaveSetting!.AnnualActive = leaveSettingUpdateDto.Annual.Active;
       
       if(leaveSettingUpdateDto?.Lop?.Days != null) leaveSetting!.LopActive = leaveSettingUpdateDto.Lop.Active;
       if(leaveSettingUpdateDto?.Lop?.EarnedLeave != null) leaveSetting!.LopDays = leaveSettingUpdateDto.Lop.Days;
       if(leaveSettingUpdateDto?.Lop?.CarryForward != null) leaveSetting!.LopCarryForward = leaveSettingUpdateDto.Lop.CarryForward;
       if(leaveSettingUpdateDto?.Lop?.CarryForwardMax != null) leaveSetting!.LopEarnedLeave = leaveSettingUpdateDto.Lop.EarnedLeave;
       if(leaveSettingUpdateDto?.Lop?.Active != null) leaveSetting!.LopCarryForwardMax = leaveSettingUpdateDto.Lop.CarryForwardMax;
       
       if(leaveSettingUpdateDto?.Maternity?.Active != null) leaveSetting!.MaternityActive = leaveSettingUpdateDto.Maternity.Active;
       if(leaveSettingUpdateDto?.Annual?.Days != null) leaveSetting!.MaternityDays = leaveSettingUpdateDto.Maternity!.Days;
       
       if(leaveSettingUpdateDto?.Paternity?.Active != null) leaveSetting!.PaternityActive = leaveSettingUpdateDto.Paternity.Active;
       if(leaveSettingUpdateDto?.Paternity?.Days != null) leaveSetting!.PaternityDays = leaveSettingUpdateDto.Paternity.Days;
       
       if(leaveSettingUpdateDto?.Sick?.Days != null) leaveSetting!.SickActive = leaveSettingUpdateDto.Sick.Active;
       if(leaveSettingUpdateDto?.Sick?.Days != null) leaveSetting!.SickDays = leaveSettingUpdateDto.Sick.Days;
       
       if(leaveSettingUpdateDto?.Hospitalisation?.Days != null) leaveSetting!.HospitalisationActive = leaveSettingUpdateDto.Hospitalisation.Active;
       if(leaveSettingUpdateDto?.Hospitalisation?.Days != null) leaveSetting!.HospitalisationDays = leaveSettingUpdateDto.Hospitalisation.Days;
    }

    public void Delete(int id)
        {
            var leaveSetting = _leaveSettingRepo.GetById(id);
            if (leaveSetting.Result != null) _leaveSettingRepo.Delete(leaveSetting.Result);
        }

        public LeaveSettingReadDto? Get(int id)
        {
            var leaveSetting = _leaveSettingRepo.GetById(id);
            if (leaveSetting.Result == null) return null;

            var annual = new AnnualDto()
            {
                Active = leaveSetting.Result.AnnualActive,
                CarryForward = leaveSetting.Result.AnnualCarryForward,
                CarryForwardMax = leaveSetting.Result.AnnualCarryForwardMax,
                Days = leaveSetting.Result.AnnualDays,
                EarnedLeave = leaveSetting.Result.AnnualEarnedLeave
            };
            
            var maternity = new MaternityDto()
            {
                Active = leaveSetting.Result.MaternityActive,
                Days = leaveSetting.Result.MaternityDays
            };
            
            var paternity = new PaternityDto()
            {
                Active = leaveSetting.Result.PaternityActive,
                Days = leaveSetting.Result.PaternityDays
            };
            var hospitalisation = new HospitalisationDto()
            {
                Active = leaveSetting.Result.HospitalisationActive,
                Days = leaveSetting.Result.HospitalisationDays
            };
            
            var lop = new LopDto()
            {
                Active = leaveSetting.Result.LopActive,
                CarryForward = leaveSetting.Result.LopCarryForward,
                CarryForwardMax = leaveSetting.Result.LopCarryForwardMax,
                Days = leaveSetting.Result.LopDays,
                EarnedLeave = leaveSetting.Result.LopEarnedLeave
            };
            var sick = new SickDto()
            {
                Active = leaveSetting.Result.SickActive,
                Days = leaveSetting.Result.SickDays
            };

            var leaveSettingReadDto = new LeaveSettingReadDto()
            {
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
