using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPaymentRepo :IGenericRepo<Payment>
{
    IQueryable<Payment> GlobalSearch(string? searchKey);
    Payment GetPaymentWithClient(int id);
    Task<List<Payment>> GetAllPaymentWithClients();
}