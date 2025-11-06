using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
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
}