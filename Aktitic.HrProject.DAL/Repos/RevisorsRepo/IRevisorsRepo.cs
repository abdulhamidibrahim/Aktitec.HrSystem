using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IRevisorsRepo :IGenericRepo<Revisor>
{
    Task<Revisor?> GetRevisorByUserIdAsync(int userId, int documentId);
    public bool RevisionCommited(int documentId);
    public void CreateDigitalSignature(int documentId);
}