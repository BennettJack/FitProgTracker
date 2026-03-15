using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;

using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class EquipmentService(FptDbContext context) 
    : BaseService<Equipment>(context), IEquipmentService
{

    //TODO fix null
    public async Task<List<DropdownReturnDto>> GetListAsDropdownAsync()
    {
        var equipment = await GetAllAsync();
        
        if(equipment.Count == 0)
            return null;

        var dropdownList = equipment.Select(eq => 
            new DropdownReturnDto { Value = eq.Id, Label = eq.Name }).ToList();
        
        return dropdownList;
    }
    

    public async Task<Equipment> AddAsync(AddExerciseRequestDto dto)
    {
        throw new NotImplementedException();
    }
}