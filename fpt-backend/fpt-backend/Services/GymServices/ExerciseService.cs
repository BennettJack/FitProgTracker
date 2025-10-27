using fpt_backend.Controllers;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.UserModels;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.UnitOfWork;

namespace fpt_backend.Services.GymServices;

public class ExerciseService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ExerciseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork =  unitOfWork;
    }
    public async Task<Result<Exercise>> AddExercise(AddExerciseRequestDto exerciseDto, string userName)
    {
        /*Exercise exercise = new Exercise
        {
            ExerciseName = exerciseDto.ExerciseName,
            ExerciseDescription = exerciseDto.Description,
            Muscles = await _muscleRepository.GetMultipleMusclesById(exerciseDto.MuscleIds),
            Equipment = await _equipmentRepository.GetMultipleEquipmentById(exerciseDto.EquipmentIds),
            Created = DateTime.Now,
            Modified = DateTime.Now,
            CreatedBy = userName,
            GloballyVisible = true
        };

        try
        {
            var createdExercise = await _exerciseRepository.AddExercise(exercise);
            return Result<Exercise>.Ok(createdExercise);
        }
        catch (Exception ex)
        {
            return Result<Exercise>.Fail("Failed to add exercise: " + ex.Message);
        }*/
        throw new NotImplementedException();
    }
}