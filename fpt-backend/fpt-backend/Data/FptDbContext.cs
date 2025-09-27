using fpt_backend.Data.Models.GymModels;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Data;

public class FptDbContext : DbContext
{
    public FptDbContext(DbContextOptions<FptDbContext> options) : base(options)
    {
        
    }
    
    //Models
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Muscle> Muscles { get; set; }
    public DbSet<ExerciseSession> ExerciseSessions { get; set; }
    public DbSet<MuscleGroup> MuscleGroups { get; set; }
    public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    
    
}