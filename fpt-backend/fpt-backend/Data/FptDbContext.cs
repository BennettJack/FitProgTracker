using fpt_backend.Data.Models;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Instances;
using fpt_backend.Data.Models.GymModels.JoiningModels;
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
    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }
    public virtual DbSet<WorkoutProgramme> WorkoutProgrammes { get; set; }
    
    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<Set> Sets { get; set; }
    public virtual DbSet<SetBloc> SetBlocs { get; set; }
    
    public virtual DbSet<SessionTemplateSetBlocTemplate> SessionTemplateSetBlocTemplate { get; set; }
    public virtual DbSet<WorkoutProgrammeTemplateSessionTemplate> WorkoutProgrammeTemplateSessionTemplate { get; set; }
    
    public virtual DbSet<ExerciseSetRecord> ExerciseSetRecord { get; set; }
    
    public virtual DbSet<ExerciseSetBlocTemplate> ExerciseSetBlocTemplate { get; set; }
    public virtual DbSet<ExerciseSetTemplate> ExerciseSetTemplate { get; set; }
    public virtual DbSet<SessionTemplate> SessionTemplate { get; set; }
    public virtual DbSet<SetTemplate> SetTemplate { get; set; }
    public virtual DbSet<WorkoutProgrammeTemplate> WorkoutProgrammeTemplate { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<Session>()
            .HasOne(s => s.WorkoutProgramme)
            .WithMany(p => p.Sessions)
            .HasForeignKey(s => s.WorkoutProgrammeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SetBloc>()
            .HasOne(sb => sb.Session)
            .WithMany(s => s.SetBlocs)
            .HasForeignKey(sb => sb.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Set>()
            .HasOne(s => s.SetBloc)
            .WithMany(sb => sb.Sets)
            .HasForeignKey(s => s.SetBlocId)
            .OnDelete(DeleteBehavior.Cascade);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.Id))
                    .ValueGeneratedOnAdd();
            }
        }
    }
}