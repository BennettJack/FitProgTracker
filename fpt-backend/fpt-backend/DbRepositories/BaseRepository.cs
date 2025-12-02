using System.Linq.Expressions;
using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly FptDbContext Context;
    protected readonly DbSet<T> DbSet;
    
    public BaseRepository(FptDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }
    
    public virtual async Task<OperationResult<List<T>>> GetAllAsync()
    {
        var items = await DbSet.ToListAsync();
        return OperationResult<List<T>>.Success(items);
    }

    public virtual async Task<OperationResult<T>> GetByIdAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null)
        {
            return OperationResult<T>.NotFound("");
        }

        return OperationResult<T>.Success(entity);
    }

    public virtual async Task<OperationResult<T>> UpdateAsync(T entity)
    {
        var entityToUpdate = await DbSet.FindAsync(entity);
        if (entityToUpdate == null)
        {
            return OperationResult<T>.NotFound("Did not find entity");
        }
        Context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        return OperationResult<T>.Success(entityToUpdate);
    }

    public virtual async Task<OperationResult<T>> DeleteAsync(T entity)
    {
        var entityToDelete = await DbSet.FindAsync(entity);
        if (entityToDelete == null)
        {
            return OperationResult<T>.NotFound("");
        }
        
        DbSet.Remove(entityToDelete);
        return OperationResult<T>.Success(entity);
    }

    public virtual async Task<OperationResult<T>> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return OperationResult<T>.Success(entity);
    }

    public async Task<OperationResult<List<T>>> AddMultipleAsync(List<T> entities)
    {
        foreach (var entity in entities)
        {
            await DbSet.AddAsync(entity);
        }

        return OperationResult<List<T>>.Success(entities);
    }

    public virtual async Task<OperationResult<T>> FindAsync(T entity)
    {
        var entityToFind = await DbSet.FindAsync(entity);
        if (entityToFind == null)
        {
            return OperationResult<T>.NotFound("NotFound");
        }
        
        return OperationResult<T>.Success(entityToFind);
    }
}