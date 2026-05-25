using fpt_backend.Data.Models;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Instances;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Data;

public class FptDbContext : DbContext
{
    public FptDbContext(DbContextOptions<FptDbContext> options)
        : base(options) { }

    //Models
    public virtual DbSet<Exercise> Exercises { get; set; }
    public virtual DbSet<Equipment> Equipment { get; set; }
    public virtual DbSet<Muscle> Muscles { get; set; }
    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }
    public virtual DbSet<WorkoutProgramme> WorkoutProgrammes { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<Set> Sets { get; set; }
    public virtual DbSet<SetBloc> SetBlocs { get; set; }

    public virtual DbSet<ExerciseSetRecord> ExerciseSetRecord { get; set; }

    public virtual DbSet<SetBlocTemplate> SetBlocTemplates { get; set; }
    public virtual DbSet<SessionTemplate> SessionTemplates { get; set; }
    public virtual DbSet<SetTemplate> SetTemplates { get; set; }
    public virtual DbSet<WorkoutProgrammeTemplate> WorkoutProgrammeTemplates { get; set; }

    public virtual DbSet<FiveThreeOneTracker> FiveThreeOneTrackers { get; set; }
    public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Session>()
            .HasOne(s => s.WorkoutProgramme)
            .WithMany(p => p.Sessions)
            .HasForeignKey(s => s.WorkoutProgrammeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<SetBloc>()
            .HasOne(sb => sb.Session)
            .WithMany(s => s.SetBlocs)
            .HasForeignKey(sb => sb.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Set>()
            .HasOne(s => s.SetBloc)
            .WithMany(sb => sb.Sets)
            .HasForeignKey(s => s.SetBlocId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<SessionTemplate>()
            .HasOne(st => st.WorkoutProgrammeTemplate)
            .WithMany(pt => pt.SessionTemplates)
            .HasForeignKey(st => st.WorkoutProgrammeTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<SetBlocTemplate>()
            .HasOne(sbt => sbt.SessionTemplate)
            .WithMany(st => st.SetBlocTemplates)
            .HasForeignKey(sbt => sbt.SessionTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<SetTemplate>()
            .HasOne(st => st.SetBlocTemplate)
            .WithMany(sbt => sbt.SetTemplates)
            .HasForeignKey(st => st.SetBlocTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<SetBloc>()
            .HasOne(sb => sb.Exercise)
            .WithMany()
            .HasForeignKey(sb => sb.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<SetBlocTemplate>()
            .HasOne(sb => sb.Exercise)
            .WithMany()
            .HasForeignKey(sb => sb.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<WorkoutProgramme>()
            .HasOne(p => p.WorkoutProgrammeTemplate)
            .WithMany()
            .HasForeignKey(p => p.WorkoutProgrammeTemplateID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Session>()
            .HasOne(s => s.SessionTemplate)
            .WithMany()
            .HasForeignKey(s => s.SessionTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<SetBloc>()
            .HasOne(sb => sb.SetBlocTemplate)
            .WithMany()
            .HasForeignKey(sb => sb.SetBlocTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Muscle>()
            .HasOne(m => m.MuscleGroup)
            .WithMany(mg => mg.Muscles)
            .HasForeignKey(m => m.MuscleGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Exercise>()
            .HasMany(e => e.Equipment)
            .WithMany(eq => eq.Exercises)
            .UsingEntity(j => j.ToTable("ExerciseEquipment"));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.Id))
                    .ValueGeneratedOnAdd();
            }
        }

        modelBuilder
            .Entity<ExerciseType>()
            .HasData(
                new ExerciseType { Id = 1, ExerciseTypeName = "Standard" },
                new ExerciseType { Id = 2, ExerciseTypeName = "Cardio" },
                new ExerciseType { Id = 3, ExerciseTypeName = "FiveThreeOne" }
            );

        modelBuilder
            .Entity<MuscleGroup>()
            .HasData(
                new MuscleGroup { Id = 1, MuscleGroupName = "Shoulder" },
                new MuscleGroup { Id = 2, MuscleGroupName = "Upper Back" },
                new MuscleGroup { Id = 3, MuscleGroupName = "Lower Back" },
                new MuscleGroup { Id = 4, MuscleGroupName = "Arms" },
                new MuscleGroup { Id = 5, MuscleGroupName = "Legs" },
                new MuscleGroup { Id = 6, MuscleGroupName = "Chest" },
                new MuscleGroup { Id = 7, MuscleGroupName = "Core" }
            );

        modelBuilder
            .Entity<Equipment>()
            .HasData(
                new Equipment() { Id = 1, Name = "Barbell" },
                new Equipment() { Id = 2, Name = "Bench" },
                new Equipment() { Id = 3, Name = "Squat Rack" }
            );

        modelBuilder
            .Entity<Muscle>()
            .HasData(
                // Shoulders (1)
                new Muscle
                {
                    Id = 1,
                    MuscleName = "Front Delts",
                    MuscleGroupId = 1,
                },
                new Muscle
                {
                    Id = 2,
                    MuscleName = "Side Delts",
                    MuscleGroupId = 1,
                },
                new Muscle
                {
                    Id = 3,
                    MuscleName = "Rear Delts",
                    MuscleGroupId = 1,
                },
                new Muscle
                {
                    Id = 4,
                    MuscleName = "Rotator Cuff",
                    MuscleGroupId = 1,
                },
                // Upper Back (2)
                new Muscle
                {
                    Id = 5,
                    MuscleName = "Lats",
                    MuscleGroupId = 2,
                },
                new Muscle
                {
                    Id = 6,
                    MuscleName = "Traps",
                    MuscleGroupId = 2,
                },
                new Muscle
                {
                    Id = 7,
                    MuscleName = "Rhomboids",
                    MuscleGroupId = 2,
                },
                new Muscle
                {
                    Id = 8,
                    MuscleName = "Teres Major",
                    MuscleGroupId = 2,
                },
                new Muscle
                {
                    Id = 9,
                    MuscleName = "Rear Delts",
                    MuscleGroupId = 2,
                },
                // Lower Back (3)
                new Muscle
                {
                    Id = 10,
                    MuscleName = "Erector Spinae",
                    MuscleGroupId = 3,
                },
                new Muscle
                {
                    Id = 11,
                    MuscleName = "Lower Back",
                    MuscleGroupId = 3,
                },
                // Arms (4)
                new Muscle
                {
                    Id = 12,
                    MuscleName = "Biceps",
                    MuscleGroupId = 4,
                },
                new Muscle
                {
                    Id = 13,
                    MuscleName = "Triceps",
                    MuscleGroupId = 4,
                },
                new Muscle
                {
                    Id = 14,
                    MuscleName = "Forearms",
                    MuscleGroupId = 4,
                },
                new Muscle
                {
                    Id = 15,
                    MuscleName = "Brachialis",
                    MuscleGroupId = 4,
                },
                new Muscle
                {
                    Id = 16,
                    MuscleName = "Brachioradialis",
                    MuscleGroupId = 4,
                },
                // Legs (5)
                new Muscle
                {
                    Id = 17,
                    MuscleName = "Quads",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 18,
                    MuscleName = "Hamstrings",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 19,
                    MuscleName = "Glutes",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 20,
                    MuscleName = "Calves",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 21,
                    MuscleName = "Hip Flexors",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 22,
                    MuscleName = "Adductors (Inner Thigh)",
                    MuscleGroupId = 5,
                },
                new Muscle
                {
                    Id = 23,
                    MuscleName = "Abductors (Outer Thigh)",
                    MuscleGroupId = 5,
                },
                // Chest (6)
                new Muscle
                {
                    Id = 24,
                    MuscleName = "Upper Chest",
                    MuscleGroupId = 6,
                },
                new Muscle
                {
                    Id = 25,
                    MuscleName = "Mid Chest",
                    MuscleGroupId = 6,
                },
                new Muscle
                {
                    Id = 26,
                    MuscleName = "Lower Chest",
                    MuscleGroupId = 6,
                },
                new Muscle
                {
                    Id = 27,
                    MuscleName = "Pecs",
                    MuscleGroupId = 6,
                },
                // Core (7)
                new Muscle
                {
                    Id = 28,
                    MuscleName = "Upper Abs",
                    MuscleGroupId = 7,
                },
                new Muscle
                {
                    Id = 29,
                    MuscleName = "Lower Abs",
                    MuscleGroupId = 7,
                },
                new Muscle
                {
                    Id = 30,
                    MuscleName = "Obliques",
                    MuscleGroupId = 7,
                },
                new Muscle
                {
                    Id = 31,
                    MuscleName = "Transverse Abs",
                    MuscleGroupId = 7,
                }
            );
    }
}
