using System.Linq.Expressions;

namespace fpt_backend.DbRepositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(T entity);
    Task<T> AddAsync(T entity);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
}