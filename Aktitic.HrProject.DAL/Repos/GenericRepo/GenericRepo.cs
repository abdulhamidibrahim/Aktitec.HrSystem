using Aktitic.HrProject.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly HrSystemDbContext _context;

    public GenericRepo(HrSystemDbContext context)
    {
        _context = context;
    }

    public Task<List<T>> GetAll()
    { 
        return Task.FromResult(_context.Set<T>()
            .AsNoTracking()
            .ToList());
    }

    public T? GetById(int? id) => _context.Set<T>().Find(id);
                 
    

    public void Add(T entity) =>_context.Set<T>().Add(entity);
         
    

    public void Update(T entity) => _context.Set<T>().Update(entity);
        
    public async void Delete(T entity) => _context.Remove(entity);
        
    // soft delete
    
    public async void SoftDelete(int id) => _context.Set<T>()
        .Update(_context.Set<T>()
            .Find(id) ?? throw new InvalidOperationException("Entity not found"));

    public void Delete(int id) =>  _context.Set<T>()
        .Remove(_context.Set<T>().Find(id) 
                ?? throw new InvalidOperationException
                    ("Entity not found"));
    
    
}