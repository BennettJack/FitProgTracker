using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories.GymRepositories;

public class MuscleRepository
{
    private readonly FptDbContext _context;

    public MuscleRepository(FptDbContext context)
    {
        _context = context;
    }

    public async Task<List<Muscle>> GetMultipleMusclesById(List<int> muscleIds)
    {
        return await _context.Muscles.Where(muscle => muscleIds.Contains
            (muscle.MuscleId)).ToListAsync();
    }

    public async Task<List<Muscle>> GetAllMuscles()
    {
        return await _context.Muscles.ToListAsync();
    }
}