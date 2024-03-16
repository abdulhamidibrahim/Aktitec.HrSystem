using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredClientDto
{
    public IEnumerable<ClientDto> ClientDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}