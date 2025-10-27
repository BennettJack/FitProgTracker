using fpt_backend.Controllers;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<Result<List<T>>> GetAll();
    
    public Task<Result<T>> GetById(int id);
}