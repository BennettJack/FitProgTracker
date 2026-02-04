using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IWorkoutProgrammeService  : IBaseService<WorkoutProgramme>
{
    Task<WorkoutProgramme> AddAsync(WorkoutProgrammeCreateRequest req);
}