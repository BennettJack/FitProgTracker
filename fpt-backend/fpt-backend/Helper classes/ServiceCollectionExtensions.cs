using fpt_backend.Controllers;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.Services.GymServices;

namespace fpt_backend.Helper_classes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //Gym Services
        services.AddScoped<EquipmentService>();
        services.AddScoped<ExerciseService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        //Gym Repositories
        services.AddScoped<EquipmentRepository>();
        services.AddScoped<ExerciseRepository>();
        services.AddScoped<MuscleRepository>();
        return services;
    }
}