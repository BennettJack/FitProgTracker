using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IExerciseService : IBaseService<Exercise>
{
    public Task<OperationResult<Exercise>> AddAsync(AddExerciseRequestDto dto, string userName);
}