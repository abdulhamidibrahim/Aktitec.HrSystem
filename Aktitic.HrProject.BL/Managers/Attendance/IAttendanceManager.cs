using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IAttendanceManager
{
    public void Add(AttendanceAddDto attendanceAddDto);
    public void Update(AttendanceUpdateDto attendanceUpdateDto);
    public void Delete(AttendanceDeleteDto attendanceDeleteDto);
    public AttendanceReadDto? Get(int id);
    public List<AttendanceReadDto> GetAll();
}