using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredSchedulingDto
{
    public IEnumerable<SchedulingReadDto> SchedulingReadDto { get; set; }
    public IEnumerable<ScheduleDto> ScheduleDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}