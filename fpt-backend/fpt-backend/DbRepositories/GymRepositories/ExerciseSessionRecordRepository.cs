using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class ExerciseSessionRecordRepository: BaseRepository<ExerciseSessionRecord>, IExerciseSessionRecordRepository
{
    public ExerciseSessionRecordRepository(FptDbContext context) : base(context)
    {
    }
}