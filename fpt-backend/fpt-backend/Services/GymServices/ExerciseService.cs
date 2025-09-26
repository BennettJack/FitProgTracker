using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories;

namespace fpt_backend.Services.GymServices;

public class ExerciseService
{
    private readonly ExerciseRepository _exerciseRepository;
    private readonly MuscleRepository _muscleRepository;
    
    public ExerciseService(ExerciseRepository exerciseRepository, 
        MuscleRepository muscleRepository)
    {
        _exerciseRepository = exerciseRepository;
        _muscleRepository = muscleRepository;
    }
    public async Task AddExercise(AddExerciseRequestDto exerciseDto)
    {
        Exercise exercise = new Exercise
        {
            ExerciseName = exerciseDto.ExerciseName,
            ExerciseDescription = exerciseDto.Description,
            Muscles = await _muscleRepository.GetMultipleMusclesById(exerciseDto.MuscleIds)
            
            
        };
        
    }
}