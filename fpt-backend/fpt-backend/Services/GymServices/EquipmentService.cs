using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.DbRepositories.UnitOfWork;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class EquipmentService : IEquipmentService
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
        
        return res.Data;
    }

    public async Task<Equipment> AddEquipment(Equipment equipment)
    {
        var res = await _equipmentRepository.AddAsync(equipment);
        await _unitOfWork.CompleteAsync();
        return res.Data;
    }
    
    public async Task<OperationResult<List<DropdownReturnDto>>> GetEquipmentListAsDropdown()
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

    public async Task<OperationResult<List<Equipment>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Equipment>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<List<Equipment>>> GetMultipleById(IEnumerable<int> ids)
    {
        var res = _equipmentRepository.GetMultipleByIdAsync(ids);
        if (ids.Any())
        {
            
        }
    }
}