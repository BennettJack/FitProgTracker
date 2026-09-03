using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<RecordsReturnDto> GetTodayRecordAsync(int sessionId)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var bySetId = await Context
            .ExerciseSetRecord.Where(r =>
                r.Set != null
                && r.Set.SetBloc.SessionId == sessionId
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

        return new RecordsReturnDto { SetRecords = bySetId };
    }

    public async Task<RecordsReturnDto> GetMostRecentRecordsAsync(List<int> exerciseIds)
    {
        var today = DateTime.Today;
        var zeroDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

        var records = await Context
            .ExerciseSetRecord.Where(r =>
                exerciseIds.Contains(r.ExerciseId)
                && r.CreatedBy == CurrentUser.UserId
                && r.Created < zeroDate
            )
            .GroupBy(r => r.ExerciseId)
            .Select(g => g.OrderByDescending(r => r.Created).First())
            .ToListAsync();

        return new RecordsReturnDto
        {
            SetRecords = records.ToDictionary(r => r.ExerciseId, r => r),
        };
    }

    public async Task<ExerciseSetRecord?> UpdateAsync(ExerciseSetRecordCreateRequest req)
    {
        var record = await Context.ExerciseSetRecord.FindAsync(req.Id);
        if (record == null)
            return null;

        record.Weight = req.Weight;
        record.RepsCompleted = req.RepsCompleted;
        record.PerceivedEffort = req.PerceivedEffort;

        await Context.SaveChangesAsync();
        return record;
    }
}
