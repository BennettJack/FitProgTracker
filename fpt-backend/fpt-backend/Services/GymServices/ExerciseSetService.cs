using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetService : IExerciseSetService
{
    public async Task<OperationResult<List<ExerciseSet>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSet>>> GetMultipleById(List<int> ids)
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

    public async Task<OperationResult<ExerciseSet>> AddAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> UpdateAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSet>> FindAsync(ExerciseSet entity)
    {
        throw new NotImplementedException();
    }
}