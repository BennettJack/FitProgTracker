using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;

using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class MuscleService : BaseService<Muscle>, IMuscleService
{

    
    public MuscleService(
        FptDbContext context) : base(context)
    {

    }
    
}