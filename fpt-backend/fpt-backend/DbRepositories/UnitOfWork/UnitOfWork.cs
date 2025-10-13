using fpt_backend.Data;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly FptDbContext _context;
    
    public IEquipmentRepository EquipmentRepository { get; }
    public IExerciseRepository ExerciseRepository { get; }
    public IMuscleRepository MuscleRepository { get; }

    public UnitOfWork(FptDbContext context)
    {
        _context = context;
        
        EquipmentRepository = new EquipmentRepository(_context);
        ExerciseRepository = new ExerciseRepository(_context);
        MuscleRepository = new MuscleRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}