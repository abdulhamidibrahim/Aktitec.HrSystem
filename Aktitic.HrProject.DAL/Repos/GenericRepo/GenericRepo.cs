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

    public async Task<List<T>> GetAll()
    { 
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public T? GetById(int? id) => _context.Set<T>().Find(id);
                 
    

    public async void Add(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);
        
    public void Delete(T entity) => _context.Remove(entity);
        
    // soft delete
    
    public async void SoftDelete(int id) => _context.Set<T>()
        .Update(await _context.Set<T>()
            .FindAsync(id) ?? throw new InvalidOperationException("Entity not found"));

    public void Delete(int id) =>  _context.Set<T>()
        .Remove(_context.Set<T>().Find(id) 
                ?? throw new InvalidOperationException
                    ("Entity not found"));
    
    
}