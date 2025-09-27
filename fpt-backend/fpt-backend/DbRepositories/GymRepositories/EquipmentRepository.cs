using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories.GymRepositories;

public class EquipmentRepository
{
    private readonly FptDbContext _context;
    
    public EquipmentRepository(FptDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Equipment>> GetAllEquipment()
    {
        return await _context.Equipment.ToListAsync();
    }

    public async Task<Equipment?> GetEquipment(int id)
    {
        return await _context.Equipment.FindAsync(id);
    }
    
    public async Task<List<Equipment>> GetMultipleEquipmentById(List<int> equipmentIds)
    {
        return await _context.Equipment.Where(equip => equipmentIds.Contains
            (equip.EquipmentId)).ToListAsync();
    }
}