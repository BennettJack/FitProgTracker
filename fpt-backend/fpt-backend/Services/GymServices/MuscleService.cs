using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;
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

    public async Task<Result<List<DropdownReturnDto>>> GetMuscleListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var res = await _muscleRepository.GetAllAsync();
        var muscles = res.Entity;
        if(muscles != null){
            
            foreach (var muscle in muscles)
            {
                dropdownList.Add(new DropdownReturnDto
                {
                    Value = muscle.MuscleId,
                    Label = muscle.MuscleName
                });
            }
            return Result<List<DropdownReturnDto>>.Ok(dropdownList);
        }
        
        return Result<List<DropdownReturnDto>>.Fail("No muscles found");
    }

    public Task<Result<List<Muscle>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Muscle>> GetById(int id)
    {
        throw new NotImplementedException();
    }
}