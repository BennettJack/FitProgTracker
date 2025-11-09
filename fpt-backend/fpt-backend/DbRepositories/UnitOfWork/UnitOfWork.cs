using fpt_backend.Data;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly FptDbContext _context;

    public UnitOfWork(
        FptDbContext context)
    {
        _context = context;

    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}