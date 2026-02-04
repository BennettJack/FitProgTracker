using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetService : BaseService<Set>, IExerciseSetService
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

    public async Task<Set> AddAsync(ExerciseSetCreationDto exerciseSet)
    {
        throw new NotImplementedException();
    }
}