using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
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

    public async Task<OperationResult<List<ExerciseSessionRecord>>> GetMultipleById(List<int> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSessionRecord>> AddAsync(ExerciseSessionRecord entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSessionRecord>> UpdateAsync(ExerciseSessionRecord entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSessionRecord>> FindAsync(ExerciseSessionRecord entity)
    {
        throw new NotImplementedException();
    }
}