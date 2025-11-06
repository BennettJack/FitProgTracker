using System.Linq.Expressions;
using fpt_backend.Data;
using fpt_backend.DbRepositories.Interfaces;
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
    
    public virtual async Task<RepositoryResult<IEnumerable<T>, RepositoryResultStatus>> GetAllAsync()
    {
        var items = await DbSet.ToListAsync();
        return RepositoryResult<IEnumerable<T>, RepositoryResultStatus>.Ok(items);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> GetByIdAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null)
        {
            return RepositoryResult<T, RepositoryResultStatus>.NotFound(RepositoryResultStatus.NotFound);
        }

        return RepositoryResult<T, RepositoryResultStatus>.Ok(entity);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> UpdateAsync(T entity)
    {
        var entityToUpdate = await DbSet.FindAsync(entity);
        if (entityToUpdate == null)
        {
            return RepositoryResult<T, RepositoryResultStatus>.NotFound(RepositoryResultStatus.NotFound);
        }
        Context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        return RepositoryResult<T, RepositoryResultStatus>.Ok(entityToUpdate);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> DeleteAsync(T entity)
    {
        var entityToDelete = await DbSet.FindAsync(entity);
        if (entityToDelete == null)
        {
            return RepositoryResult<T, RepositoryResultStatus>.NotFound(RepositoryResultStatus.NotFound);
        }
        
        DbSet.Remove(entityToDelete);
        return RepositoryResult<T, RepositoryResultStatus>.Ok(entity);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return RepositoryResult<T, RepositoryResultStatus>.Ok(entity);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> FindAsync(T entity)
    {
        var entityToFind = await DbSet.FindAsync(entity);
        if (entityToFind == null)
        {
            return RepositoryResult<T, RepositoryResultStatus>.NotFound(RepositoryResultStatus.NotFound);
        }
        
        return RepositoryResult<T, RepositoryResultStatus>.Ok(entityToFind);
    }
}