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


    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        var obj = await _dbSet.FindAsync(_context.Entry(entity).Entity);
        if (obj == null)
            return null;
        
        _context.Entry(obj).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}