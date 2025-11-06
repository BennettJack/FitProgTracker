using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
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
}