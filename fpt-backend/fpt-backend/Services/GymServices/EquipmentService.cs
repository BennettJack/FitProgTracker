using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;

namespace fpt_backend.Services.GymServices;

public class EquipmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(
        IUnitOfWork  unitOfWork,
        IEquipmentRepository equipmentRepository)
    {
        _unitOfWork = unitOfWork;
        _equipmentRepository = equipmentRepository;
    }
    
    public async Task<Equipment?> GetEquipment(int id)
    {
        var res = await _equipmentRepository.GetByIdAsync(id);
        
        return res.Entity;
    }

    public async Task<Equipment> AddEquipment(Equipment equipment)
    {
        var res = await _equipmentRepository.AddAsync(equipment);
        await _unitOfWork.CompleteAsync();
        return res.Entity;
    }
    
    public async Task<Result<List<DropdownReturnDto>>> GetEquipmentListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var res = await _equipmentRepository.GetAllAsync();
        var equipment = res.Entity;
        
        if(equipment == null)
            return Result<List<DropdownReturnDto>>.Fail("No equipment found");
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