using System.Linq.Expressions;

namespace fpt_backend.DbRepositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<RepositoryResult<IEnumerable<T>, RepositoryResultStatus>> GetAllAsync();
    Task<RepositoryResult<T, RepositoryResultStatus>> GetByIdAsync(int id);
    Task<RepositoryResult<T, RepositoryResultStatus>> UpdateAsync(T entity);
    Task<RepositoryResult<T, RepositoryResultStatus>> DeleteAsync(T entity);
    Task<RepositoryResult<T, RepositoryResultStatus>> AddAsync(T entity);
    Task<RepositoryResult<T, RepositoryResultStatus>> FindAsync(Expression<Func<T, bool>?> predicate);
}