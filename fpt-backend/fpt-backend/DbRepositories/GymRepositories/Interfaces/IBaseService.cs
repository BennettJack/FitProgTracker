using System.Linq.Expressions;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Helper_classes;

namespace fpt_backend.DbRepositories.Interfaces;

public interface IBaseService<T> where T : class
{
    Task<OperationResult<List<T>>> GetAllAsync();
    Task<OperationResult<T>> GetByIdAsync(int id);
    public Task<OperationResult<List<T>>> GetById(List<int> ids);
    Task<OperationResult<T>> UpdateAsync(T entity);
    Task<OperationResult<T>> DeleteAsync(T entity);
    Task<OperationResult<T>> AddAsync(T entity);
    Task<OperationResult<List<T>>> AddMultipleAsync(List<T> entities);
    Task<OperationResult<T>> FindAsync(T entity);
    public Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown();
}