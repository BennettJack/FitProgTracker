using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Helper_classes;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IEquipmentService : IBaseService<Equipment>
{
    public Task<OperationResult<Equipment>> AddAsync(AddExerciseRequestDto dto);
}