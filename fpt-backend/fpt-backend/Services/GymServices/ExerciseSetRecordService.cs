using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetRecordService : BaseService<ExerciseSetRecord>, IExerciseSetRecordService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IExerciseSessionService _exerciseSessionService;

    public ExerciseSetRecordService(
        FptDbContext context,
        ICurrentUserService currentUserService,
        IExerciseSessionService exerciseSessionService
    )
        : base(context, currentUserService)
    {
        _currentUserService = currentUserService;
        _exerciseSessionService = exerciseSessionService;
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
            SetId = requeset.ExerciseSetId ?? null,
        };
        var res = await Context.ExerciseSetRecord.AddAsync(setRecord);
        await Context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<TodayRecordsReturnDto> GetTodayRecordAsync(int sessionId)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var bySetId = await Context
            .ExerciseSetRecord.Where(r =>
                r.Set != null
                && r.Set.SetBlocId == sessionId
                && r.Created >= today
                && r.Created < tomorrow
            )
            .GroupBy(r => r.SetId!.Value)
            .Select(g => new
            {
                SetId = g.Key,
                Record = g.OrderByDescending(r => r.Created).First(),
            })
            .ToDictionaryAsync(x => x.SetId, x => x.Record);

        var wildcardSets = await Context
            .ExerciseSetRecord.Where(r =>
                r.SetId == null
                && r.CreatedBy == CurrentUserId
                && r.Created >= today
                && r.Created < tomorrow
            )
            .GroupBy(r => new { r.ExerciseId, r.ExerciseTypeId })
            .Select(g => new WildcardSetReturnDto
            {
                ExerciseId = g.Key.ExerciseId,
                ExerciseTypeId = g.Key.ExerciseTypeId,
                Records = g.OrderBy(r => r.Created).ToList(),
            })
            .ToListAsync();

        return new TodayRecordsReturnDto { RecordsBySetId = bySetId, WildcardSets = wildcardSets };
    }
}
