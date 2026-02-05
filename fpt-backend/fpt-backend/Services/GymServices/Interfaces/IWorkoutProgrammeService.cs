using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Dto;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IWorkoutProgrammeService  : IBaseService<WorkoutProgramme>
{
    Task<WorkoutProgrammeReturnDto?> AddAsync(WorkoutProgrammeCreateRequest req);

    Task<WorkoutProgrammeReturnDto?> GetAsDtoAsync(int id);
}