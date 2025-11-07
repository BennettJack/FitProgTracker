using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class MuscleGroupRepository : BaseRepository<MuscleGroup>, IMuscleGroupRepository
{
    public MuscleGroupRepository(FptDbContext context) : base(context)
    {
    }
}