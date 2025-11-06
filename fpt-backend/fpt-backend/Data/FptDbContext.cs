using fpt_backend.Data.Models.GymModels;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Data;

public class FptDbContext : DbContext
{
    public FptDbContext(DbContextOptions<FptDbContext> options) : base(options)
    {
        
    }
    
    //Models
    public virtual DbSet<Exercise> Exercises { get; set; }
    public virtual DbSet<Equipment> Equipment { get; set; }
    public virtual DbSet<Muscle> Muscles { get; set; }
    public virtual DbSet<ExerciseSession> ExerciseSessions { get; set; }
    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }
    public virtual DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    public virtual DbSet<ExerciseSetRecord> ExerciseSetRecords { get; set; }
    public virtual DbSet<ExerciseSet> ExerciseSets { get; set; }
    public virtual DbSet<ExerciseSessionRecord> ExerciseSessionRecords { get; set; }
    
    
}