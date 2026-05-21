using System.Linq.Expressions;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Helper_classes;

namespace fpt_backend.DbRepositories.Interfaces;

public interface IBaseService<T>
    where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    public Task<List<T>> GetByIdAsync(List<int> ids);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T> AddAsync(T entity);
    Task<List<T>> AddMultipleAsync(List<T> entities);
    Task<T> FindAsync(T entity);
    public Task<List<DropdownReturnDto>> GetListAsDropdownAsync();
    Task<T> GetByUserIdAsync(string userId);
    Task<List<T>> GetAllByUserIdAsync(string userId);
}
