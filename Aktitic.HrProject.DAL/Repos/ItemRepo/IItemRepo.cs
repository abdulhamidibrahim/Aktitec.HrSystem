using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IItemRepo :IGenericRepo<Item>
{
   List<Item> GetItemsByEstimateId(int estimateId);

   IEnumerable<Item> GetItemsByInvoiceId(int id);
}