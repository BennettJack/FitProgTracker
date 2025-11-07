using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class ExerciseSetRepository : BaseRepository<ExerciseSet>, IExerciseSetRepository
{
    public ExerciseSetRepository(FptDbContext context) : base(context)
    {
    }
}