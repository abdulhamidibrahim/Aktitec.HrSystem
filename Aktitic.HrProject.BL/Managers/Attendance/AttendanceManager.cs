
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class AttendanceManager:IAttendanceManager
{
    private readonly IAttendanceRepo _attendanceRepo;

    public AttendanceManager(IAttendanceRepo attendanceRepo)
    {
        _attendanceRepo = attendanceRepo;
    }
    
    public void Add(AttendanceAddDto attendanceAddDto)
    {
        var attendance = new Attendance()
        {
           EmployeeId = attendanceAddDto.EmployeeId,
           Break = attendanceAddDto.Break,
           Date = attendanceAddDto.Date,
           OvertimeId = attendanceAddDto.OvertimeId,
           Production = attendanceAddDto.Production,
           PunchIn = attendanceAddDto.PunchIn,
           PunchOut = attendanceAddDto.PunchOut,
        };
        _attendanceRepo.Add(attendance);
    }

    public void Update(AttendanceUpdateDto attendanceUpdateDto)
    {
        var attendance = _attendanceRepo.GetById(attendanceUpdateDto.Id);
        
        if (attendance.Result == null) return;
        attendance.Result.EmployeeId = attendanceUpdateDto.EmployeeId;
        attendance.Result.Break = attendanceUpdateDto.Break;
        attendance.Result.Date = attendanceUpdateDto.Date;
        attendance.Result.OvertimeId = attendanceUpdateDto.OvertimeId;
        attendance.Result.Production = attendanceUpdateDto.Production;
        attendance.Result.PunchIn = attendanceUpdateDto.PunchIn;
        attendance.Result.PunchOut = attendanceUpdateDto.PunchOut;
        

        _attendanceRepo.Update(attendance.Result);
    }

    public void Delete(AttendanceDeleteDto attendanceDeleteDto)
    {
        var attendance = _attendanceRepo.GetById(attendanceDeleteDto.Id);
        if (attendance.Result != null) _attendanceRepo.Delete(attendance.Result);
    }

    public AttendanceReadDto? Get(int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        if (attendance.Result == null) return null;
        return new AttendanceReadDto()
        {
            Id = attendance.Result.Id,
            EmployeeId = attendance.Result.EmployeeId,
            Break = attendance.Result.Break,
            Date = attendance.Result.Date,
            OvertimeId = attendance.Result.OvertimeId,
            Production = attendance.Result.Production,
            PunchIn = attendance.Result.PunchIn,
            PunchOut = attendance.Result.PunchOut,
        };
    }

    public List<AttendanceReadDto> GetAll()
    {
        var attendances = _attendanceRepo.GetAll();
        return attendances.Result.Select(attendance => new AttendanceReadDto()
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            Break = attendance.Break,
            Date = attendance.Date,
            OvertimeId = attendance.OvertimeId,
            Production = attendance.Production,
            PunchIn = attendance.PunchIn,
            PunchOut = attendance.PunchOut,
            
        }).ToList();
    }
}
