using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.Services.GymServices;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Helper_classes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //Gym Services
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IExerciseService, ExerciseService>();
        services.AddScoped<IMuscleService, MuscleService>();
        services.AddScoped<IExerciseSessionRecordService, ExerciseSessionRecordService>();
        services.AddScoped<IExerciseSessionService, ExerciseSessionService>();
        services.AddScoped<IMuscleGroupService, MuscleGroupService>();
        services.AddScoped<IExerciseSetService, ExerciseSetService>();
        services.AddScoped<IExerciseSetRecordService, ExerciseSetRecordService>();
        services.AddScoped<IWorkoutProgramService, WorkoutProgramService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        //Gym Repositories
        services.AddScoped<IExerciseSessionRepository, ExerciseSessionRepository>();
        services.AddScoped<IExerciseSessionRecordRepository, ExerciseSessionRecordRepository>();
        services.AddScoped<IExerciseSetRepository, ExerciseSetRepository>();
        services.AddScoped<IExerciseSetRecordRepository, ExerciseSetRecordRepository>();
        services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
        services.AddScoped<IWorkoutProgramRepository, WorkoutProgramRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IMuscleRepository, MuscleRepository>();
        return services;
    }
}