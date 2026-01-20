using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class EquipmentService(FptDbContext context) 
    : BaseService<Equipment>(context), IEquipmentService
{
    public async Task<Equipment?> GetEquipment(int id)
    {
        var res = await _equipmentRepository.GetByIdAsync(id);
        
        return res.Data;
    }

    public async Task<Equipment> AddEquipment(Equipment equipment)
    {
        var res = await _equipmentRepository.AddAsync(equipment);
        return res.Data;
    }

    public async Task<OperationResult<List<Equipment>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Equipment>>> GetById(List<int> ids)
    {
        var res = await _equipmentRepository.GetMultipleByIdAsync(ids);
        if (ids.Any())
        {
            return res;
        }
        return OperationResult<List<Equipment>>.Failure("failed");
    }

    public async Task<OperationResult<List<DropdownReturnDto>>> GetListAsDropdown()
    {
        var dropdownList = new List<DropdownReturnDto>();

        var res = await _equipmentRepository.GetAllAsync();
        var equipment = res.Data;
        
        if(equipment == null)
            return OperationResult<List<DropdownReturnDto>>.Failure("No equipment found");
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
            return OperationResult<List<DropdownReturnDto>>.Success(dropdownList);
        }
        catch (Exception ex)
        {
            return OperationResult<List<DropdownReturnDto>>.Failure(ex.Message);
        }
    }

    public async Task<OperationResult<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> AddAsync(Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Equipment>>> AddMultipleAsync(List<Equipment> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> UpdateAsync(Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> FindAsync(Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> AddAsync(AddExerciseRequestDto dto)
    {
        throw new NotImplementedException();
    }
}