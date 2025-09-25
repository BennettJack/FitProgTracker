using fpt_backend.Data;
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
}