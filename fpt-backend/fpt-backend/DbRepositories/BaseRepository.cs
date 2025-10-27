using System.Linq.Expressions;
using fpt_backend.Data;
using fpt_backend.DbRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly FptDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public BaseRepository(FptDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    private object?[] GetPrimaryKeyValue(T entity)
    {
        var type = _context.Model.FindEntityType(typeof(T))
            ?? throw new InvalidOperationException($"Model {typeof(T).Name} not found");
        
        var primaryKey = type.FindPrimaryKey()
            ?? throw new InvalidOperationException($"Model {typeof(T).Name} does not have a defined primary key");
        
        return primaryKey.Properties
            .Select(p => p.PropertyInfo!.GetValue(entity))
            .ToArray();
    }

    protected async Task<bool> ExistsAsync(T entity)
    {
        var keyValues =  GetPrimaryKeyValue(entity);
        var exists = await _dbSet.FindAsync(keyValues);
        return exists != null;
        
    }
    public virtual async Task<RepositoryResult<IEnumerable<T>, RepositoryResultStatus>> GetAllAsync()
    {
        var items = await _dbSet.AsNoTracking().ToListAsync();
        return RepositoryResult<IEnumerable<T>, RepositoryResultStatus>.Ok(items);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return RepositoryResult<T, RepositoryResultStatus>.NotFound(RepositoryResultStatus.NotFound);
        }

        return RepositoryResult<T, RepositoryResultStatus>.Ok(entity);
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> UpdateAsync(T entity)
    {
        if (!await ExistsAsync(entity))
            return null;
        var obj = _context.Entry(entity).Property("Id").CurrentValue;
        var res = await _dbSet.FindAsync(obj);
        if (res == null)
            return null;
        
        _context.Entry(res).CurrentValues.SetValues(entity);
        throw new NotImplementedException();
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<RepositoryResult<T, RepositoryResultStatus>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}