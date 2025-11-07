using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.Helper_classes;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories.GymRepositories;

public class MuscleRepository : BaseRepository<Muscle>, IMuscleRepository
{

    public MuscleRepository(FptDbContext context) : base(context)
    {
    }

    public async Task<List<Muscle>> GetByIdAsync(List<int> muscleIds)
    {
        return await DbSet.Where(muscle => muscleIds.Contains
            (muscle.MuscleId)).ToListAsync();
    }

    public async Task<OperationResult<List<Muscle>>> GetMultipleByIdAsync(IEnumerable<int> ids)
    {
        var data = await DbSet.Where(muscle => ids.Contains(muscle.MuscleId)).ToListAsync();
        return OperationResult<List<Muscle>>.Success(data);
    }
}