using fpt_backend.Data;
using fpt_backend.Data.Constants.GymConstants;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class FiveThreeOneService : BaseService<FiveThreeOneTracker>, IFiveThreeOneService
{
    public FiveThreeOneService(FptDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService) { }

    public async Task<FiveThreeOneTracker> AddAsync(CreateUpdateFiveThreeOneTrackerRequest req)
    {
        var tracker = new FiveThreeOneTracker
        {
            UserId = CurrentUserId,
            MaxType = req.MaxType,
            OverheadPressTrainingMax = req.OverheadPressWeight,
            BarbellSquatTrainingMax = req.BarbellSquatWeight,
            BenchPressTrainingMax = req.BenchPressWeight,
            DeadliftTrainingMax = req.DeadliftWeight,

            OverheadPressCycle = 1,
            BarbellSquatCycle = 1,
            BenchPressCycle = 1,
            DeadliftCycle = 1,
        };

        Context.FiveThreeOneTrackers.Add(tracker);
        await Context.SaveChangesAsync();
        return tracker;
    }

    public override async Task<FiveThreeOneTracker> GetByUserIdAsync(string userId)
    {
        var tracker = await Context.FiveThreeOneTrackers.FirstOrDefaultAsync(t =>
            t.UserId == userId
        );
        if (tracker == null)
        {
            var req = new CreateUpdateFiveThreeOneTrackerRequest()
            {
                MaxType = MaxTypes.OnePlusMax,
                OverheadPressWeight = 0,
                BarbellSquatWeight = 0,
                BenchPressWeight = 0,
                DeadliftWeight = 0,
            };
            tracker = await AddAsync(req);
        }

        return tracker;
    }
}
