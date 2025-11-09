using fpt_backend.Controllers;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class WorkoutProgramService : IWorkoutProgramService
{
    public async Task<OperationResult<List<WorkoutProgram>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<WorkoutProgram>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<WorkoutProgram>>> GetMultipleById(List<int> ids)
    {
        throw new NotImplementedException();
    }
}