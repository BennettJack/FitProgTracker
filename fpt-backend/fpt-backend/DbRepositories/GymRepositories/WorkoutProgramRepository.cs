using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class WorkoutProgramRepository : BaseRepository<WorkoutProgram>, IWorkoutProgramRepository
{
    public WorkoutProgramRepository(FptDbContext context) : base(context)
    {
    }
}