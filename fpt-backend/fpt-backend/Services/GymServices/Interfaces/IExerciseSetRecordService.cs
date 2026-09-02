using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface IExerciseSetRecordService : IBaseService<ExerciseSetRecord>
{
    public Task<ExerciseSetRecord> AddAsync(ExerciseSetRecordCreateRequest request);
    public Task<RecordsReturnDto> GetTodayRecordAsync(int sessionId);

    public Task<RecordsReturnDto> GetMostRecentRecordsAsync(List<int> exerciseIds);
}
