using fpt_backend.Controllers;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSessionRecordService : IExerciseSessionRecordService
{
    public Task<OperationResult<List<ExerciseSessionRecord>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<ExerciseSessionRecord>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSessionRecord>>> GetMultipleById(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}