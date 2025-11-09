using fpt_backend.Data;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public Task<int> CompleteAsync();
    
}