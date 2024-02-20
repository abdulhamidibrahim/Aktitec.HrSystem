using Aktitic.HrProject.DAL.Context;

namespace Aktitic.HrProject.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly HrManagementDbContext _context;

    public GenericRepo(HrManagementDbContext context)
    {
        _context = context;
    }

    public Task<List<T>> GetAll()
    { 
        return Task.FromResult(_context.Set<T>().ToList());
    }

    public Task<T?> GetById(int id)
    {
        return Task.FromResult(_context.Set<T>().Find(id));        
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();        
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _context.Set<T>().Find(id);
        
        if (entity == null) return;
        
        _context.Remove(entity);
        _context.SaveChanges();        
    }
    
}