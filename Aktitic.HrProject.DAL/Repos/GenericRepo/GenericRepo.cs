using Aktitic.HrProject.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL;

public class GenericRepo<T>(HrSystemDbContext context) : IGenericRepo<T> where T : class
{
    public async Task<List<T>> GetAll()
    { 
        return await context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
    
    // compute the size in MB of the query
    public async Task<List<T>> GetAllDeletedRecords()
    { 
        return await context.Set<T>()
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(e => EF.Property<bool>(e, "IsDeleted") == true)
            .ToListAsync();
    }

    public T? GetById(int? id) => context.Set<T>().Find(id);
                 
    

    public async void Add(T entity) => await context.Set<T>().AddAsync(entity);

    public void Update(T entity) => context.Set<T>().Update(entity);
        
    public void Delete(T entity) => context.Remove(entity);
        
    // soft delete
    
    public async void SoftDelete(int id) => context.Set<T>()
        .Update(await context.Set<T>()
            .FindAsync(id) ?? throw new InvalidOperationException("Entity not found"));

    public void Delete(int id) =>  context.Set<T>()
        .Remove(context.Set<T>().Find(id) 
                ?? throw new InvalidOperationException
                    ("Entity not found"));
    
    
}