
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Services.GymServices;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Helper_classes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //Gym Services
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
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
}