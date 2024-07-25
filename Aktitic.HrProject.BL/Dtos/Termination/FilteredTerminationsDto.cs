using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredTerminationsDto
{
    public IEnumerable<TerminationDto> TerminationDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}