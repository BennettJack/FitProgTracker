using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;

namespace fpt_backend.DbRepositories.GymRepositories.Interfaces;

public interface IMuscleRepository : IBaseRepository<Muscle>
{
    public Task<OperationResult<List<Muscle>>> GetMultipleByIdAsync(IEnumerable<int> ids);
}