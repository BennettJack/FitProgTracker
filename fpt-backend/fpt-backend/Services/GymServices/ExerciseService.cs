using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseService : BaseService<Exercise>, IExerciseService
{
    private readonly IMuscleService _muscleService;
    private readonly IEquipmentService _equipmentService;
    
    public ExerciseService(
        FptDbContext context,
        IMuscleService muscleService,
        IEquipmentService equipmentService,
        ICurrentUserService currentUserService
        ) : base(context, currentUserService)
    {
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

    public async Task<List<Exercise>> GetByIdAsync(List<int> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DropdownReturnDto>> GetListAsDropdownAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Exercise>> AddMultipleAsync(List<Exercise> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Exercise>> UpdateAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Exercise> FindAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Exercise> AddAsync(AddExerciseRequestDto dto, string userName)
    {
        Exercise exercise = new Exercise
        {
            ExerciseName = dto.Name,
            ExerciseDescription = dto.Description,
            Muscles = await _muscleService.GetByIdAsync(dto.MuscleIds),
            Equipment = await _equipmentService.GetByIdAsync(dto.EquipmentIds),
            Created = DateTime.Now,
            Modified = DateTime.Now,
            CreatedBy = userName,
            GloballyVisible = true
        };
        
        var createdExercise = await AddAsync(exercise);
        await Context.SaveChangesAsync();
        return createdExercise;
    }
}