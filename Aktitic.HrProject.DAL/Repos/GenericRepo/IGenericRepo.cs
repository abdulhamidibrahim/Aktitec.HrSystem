namespace Aktitic.HrProject.DAL;

public interface IGenericRepo<T> 
    where T : class
{
    Task<List<T>> GetAll();
    T? GetById(int? id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(int id);
    public void SoftDelete(int id);

}