using Aktitic.HrProject.DAL.Context;
using Microsoft.EntityFrameworkCore;

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
        return Task.FromResult(_context.Set<T>()
            .AsNoTracking()
            .ToList());
    }

    public Task<T?> GetById(int? id)
    {
        return Task.FromResult(
            _context.Set<T>()
            .Find(id));        
    }

    public async Task<int> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return await _context.SaveChangesAsync();        
    }

    public async Task<int> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(T entity)
    {
        _context.Remove(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        
        if (entity == null) return 0;
        
        _context.Remove(entity);
        return await _context.SaveChangesAsync();        
    }
    
}