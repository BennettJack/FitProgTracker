using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories;

namespace fpt_backend.Services.GymServices;

public class EquipmentService
{
    private readonly EquipmentRepository _equipmentRepository;

    public EquipmentService(EquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
    
    public async Task<Equipment?> GetEquipment(int id)
    {
        return await _equipmentRepository.GetEquipment(id);
    }
    
    public async Task<Result<List<DropdownReturnDto>>> GetEquipmentListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var equipment = await _equipmentRepository.GetAllEquipment();
        try
        {
            foreach (var eq in equipment)
            {
                dropdownList.Add(new DropdownReturnDto
                {
                    Value = eq.EquipmentId,
                    Label = eq.EquipmentName
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