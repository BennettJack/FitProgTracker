using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
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

    public async Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<WorkoutProgram>> AddAsync(WorkoutProgram entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<WorkoutProgram>>> AddMultipleAsync(List<WorkoutProgram> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<WorkoutProgram>> UpdateAsync(WorkoutProgram entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<WorkoutProgram>> FindAsync(WorkoutProgram entity)
    {
        throw new NotImplementedException();
    }
}