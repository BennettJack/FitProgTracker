using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetService : BaseService<ExerciseSet>, IExerciseSetService
{
    private readonly IExerciseService _exerciseService;
    private readonly IExerciseSessionService _exerciseSessionService;

    public ExerciseSetService(
        FptDbContext context,
        IExerciseService exerciseService,
        IExerciseSessionService exerciseSessionService
        ) : base(context)
    {
        _exerciseService = exerciseService;
        _exerciseSessionService = exerciseSessionService;
    }
    

    public async Task<ExerciseSet> AddAsync(ExerciseSetCreationDto exerciseSet)
    {
        var exercise = await _exerciseService.GetByIdAsync(exerciseSet.ExerciseId);

        var exerciseSession = await _exerciseSessionService.GetByIdAsync(exerciseSet.ExerciseSessionId);

        ExerciseSet set = new()
        {
            Name =  exerciseSet.Name,
            Exercise = exercise,
            RepFloor = exerciseSet.RepFloor,
            RepCeiling = exerciseSet.RepCeiling,
            ExerciseSessions = [exerciseSession]
        };
        
        var res = await AddAsync(set);
        return res;
    }
}