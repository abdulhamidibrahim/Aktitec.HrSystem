using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredPoliciesDto
{
    public IEnumerable<PolicyDto> PoliciesDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}