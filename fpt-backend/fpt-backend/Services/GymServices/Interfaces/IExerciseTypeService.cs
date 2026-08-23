using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IExerciseTypeService : IBaseService<ExerciseType>
{
    public Task<List<DropdownReturnDto>> GetExerciseTypesByExerciseAsync(int exerciseId);
}
