using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.DbRepositories.GymRepositories;

public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
{

    public ExerciseRepository(FptDbContext context) : base(context)
    {
        
    }

    public async Task GetExercise(Exercise exercise)
    {
        await Context.Exercises.AddAsync(exercise);
    }

    public async Task<Exercise> AddExercise(Exercise exercise)
    {
        Context.Exercises.Add(exercise);
        await Context.SaveChangesAsync();
        return  exercise;
    }
}