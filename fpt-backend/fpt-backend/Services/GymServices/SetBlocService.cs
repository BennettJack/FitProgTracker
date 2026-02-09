using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class SetBlocService : BaseService<SetBloc>, ISetBlocService 
{
    public SetBlocService(FptDbContext context,
        IExerciseSetService exerciseSetService) : base(context)
    {
    }
}