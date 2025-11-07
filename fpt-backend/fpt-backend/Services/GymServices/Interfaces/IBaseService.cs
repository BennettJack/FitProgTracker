using fpt_backend.Controllers;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<OperationResult<List<T>>> GetAll();

    public Task<OperationResult<T>> GetById(int id);
    public Task<OperationResult<List<T>>> GetMultipleById(IEnumerable<int> ids);

}