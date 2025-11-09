using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class MuscleGroupService : IMuscleGroupService
{
    public async Task<OperationResult<List<MuscleGroup>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<MuscleGroup>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<MuscleGroup>>> GetMultipleById(List<int> ids)
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

    public async Task<OperationResult<MuscleGroup>> AddAsync(MuscleGroup entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<MuscleGroup>> UpdateAsync(MuscleGroup entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<MuscleGroup>> FindAsync(MuscleGroup entity)
    {
        throw new NotImplementedException();
    }
}