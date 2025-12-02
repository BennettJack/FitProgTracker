using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class MuscleService : IMuscleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMuscleRepository _muscleRepository;
    
    public MuscleService(
        IUnitOfWork unitOfWork,
        IMuscleRepository muscleRepository)
    {
        _unitOfWork = unitOfWork;
        _muscleRepository = muscleRepository;
    }
    
    public Task<OperationResult<List<Muscle>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Muscle>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Muscle>>> GetMultipleById(List<int> ids)
    {
        var res = await _muscleRepository.GetMultipleByIdAsync(ids);
        if (ids.Any())
        {
            return res;
        }
        return OperationResult<List<Muscle>>.Failure("failed");
    }

    public async Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var res = await _muscleRepository.GetAllAsync();
        var muscles = res.Data;
        if(muscles != null){
            
            foreach (var muscle in muscles)
            {
                dropdownList.Add(new DropdownReturnDto
                {
                    Value = muscle.MuscleId,
                    Label = muscle.MuscleName
                });
            }
            return OperationResult<List<DropdownReturnDto>>.Success(dropdownList);
        }
        
        return OperationResult<List<DropdownReturnDto>>.Failure("No muscles found");
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Muscle>> AddAsync(Muscle entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Muscle>>> AddMultipleAsync(List<Muscle> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Muscle>> UpdateAsync(Muscle entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Muscle>> FindAsync(Muscle entity)
    {
        throw new NotImplementedException();
    }
}