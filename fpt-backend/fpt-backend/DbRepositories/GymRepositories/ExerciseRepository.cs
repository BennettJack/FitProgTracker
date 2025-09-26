using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.DbRepositories.GymRepositories;

public class ExerciseRepository
{
    private readonly FptDbContext _context;

    public ExerciseRepository(FptDbContext context)
    {
        _context = context;
    }

    public async Task GetExercise(Exercise exercise)
    {
        await _context.Exercises.AddAsync(exercise);
    }
}