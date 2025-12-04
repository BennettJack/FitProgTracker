using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetService : IExerciseSetService
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly IExerciseService _exerciseService;
    private readonly IExerciseSessionService _exerciseSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public ExerciseSetService(IExerciseSetRepository repository,
        IExerciseService exerciseService,
        IExerciseSessionService exerciseSessionService,
        IUnitOfWork unitOfWork
        )
    {
        _exerciseSetRepository = repository;
        _exerciseService = exerciseService;
        _exerciseSessionService = exerciseSessionService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OperationResult<List<ExerciseSet>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSet>>> GetMultipleById(List<int> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> AddAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSet>>> AddMultipleAsync(List<ExerciseSet> entities)
    {
        var res = _exerciseSetRepository.AddMultipleAsync(entities);
        return await res;
    }

    public async Task<OperationResult<ExerciseSet>> UpdateAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> FindAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> AddAsync(ExerciseSetCreationDto exerciseSet)
    {
        var exercise = await _exerciseService.GetById(exerciseSet.ExerciseId);
        if (exercise.Data == null)
        {
            return OperationResult<ExerciseSet>.Failure("Exercise set not found");
        }

        var exerciseSession = await _exerciseSessionService.GetById(exerciseSet.ExerciseSessionId);
        if (exerciseSession.Data == null)
        {
            return OperationResult<ExerciseSet>.Failure("Exercise session not found");
        }
        ExerciseSet set = new()
        {
            Name =  exerciseSet.Name,
            Exercise = exercise.Data,
            RepFloor = exerciseSet.RepFloor,
            RepCeiling = exerciseSet.RepCeiling,
            ExerciseSessions = [exerciseSession.Data]
        };
        
        var res = await _exerciseSetRepository.AddAsync(set);
        await _unitOfWork.CompleteAsync();
        return res;
    }
}