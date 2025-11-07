using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using fpt_backend.Helper_classes;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories.GymRepositories;

public class EquipmentRepository  : BaseRepository<Equipment>, IEquipmentRepository
{
    
    
    public EquipmentRepository(FptDbContext context) : base(context)
    {
      
    }
    
    public async Task<List<Equipment>> GetById(List<int> equipmentIds)
    {
        return await DbSet.Where(equip => equipmentIds.Contains
            (equip.EquipmentId)).ToListAsync();
    }

    public async Task<OperationResult<List<Equipment>>> GetMultipleByIdAsync(IEnumerable<int> ids)
    {
        var data = await DbSet.Where(equipment => ids.Contains(equipment.EquipmentId)).ToListAsync();
        return OperationResult<List<Equipment>>.Success(data);
    }
}