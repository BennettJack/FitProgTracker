using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class ExerciseSetRecordService : IExerciseSetRecordService
{
    public async Task<OperationResult<List<ExerciseSetRecord>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSetRecord>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<ExerciseSetRecord>>> GetMultipleById(List<int> ids)
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

    public async Task<OperationResult<ExerciseSetRecord>> AddAsync(ExerciseSetRecord entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSetRecord>> UpdateAsync(ExerciseSetRecord entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<ExerciseSetRecord>> FindAsync(ExerciseSetRecord entity)
    {
        throw new NotImplementedException();
    }
}