using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.UserModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseService : IExerciseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMuscleService _muscleService;
    private readonly IEquipmentService _equipmentService;
    
    public ExerciseService(IUnitOfWork unitOfWork, 
        IExerciseRepository exerciseRepository,
        IMuscleService muscleService,
        IEquipmentService equipmentService)
    {
        _unitOfWork =  unitOfWork;
        _exerciseRepository = exerciseRepository;
        _muscleService = muscleService;
        _equipmentService = equipmentService;
    }

    public async Task<OperationResult<List<Exercise>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Exercise>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Exercise>>> GetMultipleById(List<int> ids)
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

    public async Task<OperationResult<Exercise>> AddAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Exercise>> UpdateAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Exercise>> FindAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Exercise>> AddAsync(AddExerciseRequestDto dto, string userName)
    {
        Exercise exercise = new Exercise
        {
            ExerciseName = dto.ExerciseName,
            ExerciseDescription = dto.Description,
            Muscles = (await _muscleService.GetMultipleById(dto.MuscleIds)).Data,
            Equipment = (await _equipmentService.GetMultipleById(dto.EquipmentIds)).Data,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            CreatedBy = userName,
            GloballyVisible = true
        };
        
        var createdExercise = await _exerciseRepository.AddAsync(exercise);
        if (createdExercise.Data != null)
        {
            await _unitOfWork.CompleteAsync();
            return OperationResult<Exercise>.Success(createdExercise.Data);
            
        }
        return OperationResult<Exercise>.Failure("Could not add exercise");
    }
}