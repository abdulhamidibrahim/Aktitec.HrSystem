using Aktitic.HrProject.DAL.Models;
using AutoMapper;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPromotionRepo :IGenericRepo<Promotion>
{
    IQueryable<Promotion> GlobalSearch(string? searchKey);
    
    IQueryable<Promotion> GetAllWithEmployees();
    IQueryable<Promotion> GetWithEmployees(int id);
}