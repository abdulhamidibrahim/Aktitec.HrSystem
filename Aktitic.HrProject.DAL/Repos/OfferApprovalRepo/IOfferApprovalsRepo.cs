using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IOfferApprovalsRepo :IGenericRepo<OfferApproval>
{
    IQueryable<OfferApproval> GlobalSearch(string? searchKey);
    
    Task<IEnumerable<OfferApproval>> GetAllOfferApprovals();
}