using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IMuscleRepository MuscleRepository { get; }
    public IEquipmentRepository EquipmentRepository { get; }
    public IExerciseRepository ExerciseRepository { get; }

    Task<int> CompleteAsync();

}