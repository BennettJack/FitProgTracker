using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IExerciseSetService : IBaseService<ExerciseSet>
{
    public Task<ExerciseSet> AddAsync(ExerciseSetCreationDto exerciseSet);
}