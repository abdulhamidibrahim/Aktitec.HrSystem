using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredTicketDto
{
    public IEnumerable<TicketDto> TicketDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}