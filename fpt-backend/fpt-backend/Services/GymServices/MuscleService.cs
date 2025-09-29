using fpt_backend.Controllers;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.DbRepositories.GymRepositories;

namespace fpt_backend.Services.GymServices;

public class MuscleService
{
    private readonly MuscleRepository _muscleRepository;
    
    public MuscleService(MuscleRepository muscleRepository)
    {
        _muscleRepository = muscleRepository;
    }

    public async Task<Result<List<DropdownReturnDto>>> GetMuscleListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var muscles = await _muscleRepository.GetAllMuscles();
        try
        {
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
        catch (Exception ex)
        {
            return Result<List<DropdownReturnDto>>.Fail(ex.Message);
        }
    }
}