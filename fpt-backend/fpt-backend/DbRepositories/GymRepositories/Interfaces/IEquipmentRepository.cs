using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;

namespace fpt_backend.DbRepositories.GymRepositories.Interfaces;

public interface IEquipmentRepository : IBaseRepository<Equipment>
{
    public Task<OperationResult<List<Equipment>>> GetMultipleByIdAsync(IEnumerable<int> ids);
}