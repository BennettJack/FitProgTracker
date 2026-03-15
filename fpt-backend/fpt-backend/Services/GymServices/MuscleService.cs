using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;

using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class MuscleService : BaseService<Muscle>, IMuscleService
{

    
    public MuscleService(
        FptDbContext context) : base(context)
    {

    }
    
        public override async Task<List<DropdownReturnDto>> GetListAsDropdownAsync()
    {
        var equipment = await GetAllAsync();
        
        if(equipment.Count == 0)
            return null;

        var dropdownList = equipment.Select(m => 
            new DropdownReturnDto { Value = m.Id, Label = m.MuscleName }).ToList();
        
        return dropdownList;
    }
}