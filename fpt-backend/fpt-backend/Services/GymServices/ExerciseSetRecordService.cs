using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetRecordService : BaseService<ExerciseSetRecord>, IExerciseSetRecordService
{
    private readonly ICurrentUserService _currentUserService;

    public ExerciseSetRecordService(FptDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<ExerciseSetRecord> AddAsync(ExerciseSetRecordCreateRequest requeset)
    {
        var setRecord = new ExerciseSetRecord
        {
            ExerciseId = requeset.ExerciseId,
            ExerciseTypeId = requeset.ExerciseTypeId,
            Weight = requeset.Weight,
            RepsCompleted = requeset.RepsCompleted,
            CreatedBy = CurrentUserId,
            Created = DateTime.Now,
        };
        var res = await Context.ExerciseSetRecord.AddAsync(setRecord);
        await Context.SaveChangesAsync();
        return res.Entity;
    }
}
