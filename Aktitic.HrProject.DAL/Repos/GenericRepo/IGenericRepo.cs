namespace Aktitic.HrProject.DAL;

public interface IGenericRepo<T> 
    where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int? id);
    Task<int> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(T entity);
    Task<int> Delete(int id);
   
}