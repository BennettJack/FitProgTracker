using System.Linq.Expressions;
using fpt_backend.Helper_classes;

namespace fpt_backend.DbRepositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<OperationResult<List<T>>> GetAllAsync();
    Task<OperationResult<T>> GetByIdAsync(int id);
    Task<OperationResult<T>> UpdateAsync(T entity);
    Task<OperationResult<T>> DeleteAsync(T entity);
    Task<OperationResult<T>> AddAsync(T entity);
    Task<OperationResult<T>> FindAsync(T entity);
}