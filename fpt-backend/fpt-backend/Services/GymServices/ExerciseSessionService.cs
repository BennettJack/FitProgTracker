using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
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

    public async Task<OperationResult<List<ExerciseSession>>> GetMultipleById(List<int> ids)
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

    public async Task<OperationResult<ExerciseSession>> AddAsync(ExerciseSession entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSession>>> AddMultipleAsync(List<ExerciseSession> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSession>> UpdateAsync(ExerciseSession entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSession>> FindAsync(ExerciseSession entity)
    {
        throw new NotImplementedException();
    }
}