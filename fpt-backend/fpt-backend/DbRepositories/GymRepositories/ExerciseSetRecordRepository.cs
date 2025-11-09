using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class ExerciseSetRecordRepository : BaseRepository<ExerciseSetRecord>, IExerciseSetRecordRepository
{
    public ExerciseSetRecordRepository(FptDbContext context) : base(context)
    {
    }
}