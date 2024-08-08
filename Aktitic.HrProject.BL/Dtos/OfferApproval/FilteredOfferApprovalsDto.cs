using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class FilteredOfferApprovalsDto
{
    public IEnumerable<OfferApprovalDto> OfferApprovalDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}