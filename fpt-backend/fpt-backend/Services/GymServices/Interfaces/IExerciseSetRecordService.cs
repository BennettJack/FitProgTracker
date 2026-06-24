using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IExerciseSetRecordService : IBaseService<ExerciseSetRecord>
{
    public Task<ExerciseSetRecord> AddAsync(ExerciseSetRecordCreateRequest request);
}
