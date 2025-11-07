using fpt_backend.Controllers;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSessionService : IExerciseSessionService
{
    public Task<OperationResult<List<ExerciseSession>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<ExerciseSession>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSession>>> GetMultipleById(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}