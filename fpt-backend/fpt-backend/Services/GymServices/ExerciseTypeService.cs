using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class ExerciseTypeService : BaseService<ExerciseType>, IExerciseTypeService
{
    public ExerciseTypeService(FptDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService) { }

    public override async Task<List<DropdownReturnDto>> GetListAsDropdownAsync()
    {
        var entities = await DbSet.ToListAsync();
        var dropdownDtoList = new List<DropdownReturnDto>();

        foreach (var entity in entities)
        {
            dropdownDtoList.Add(
                new DropdownReturnDto { Value = entity.Id, Label = entity.ExerciseTypeName }
            );
        }
        return dropdownDtoList;
    }

    public async Task<List<DropdownReturnDto>> GetExerciseTypesByExerciseAsync(int exerciseId)
    {
        var exerciseTypes = await Context
            .ExerciseTypes.Where(et => et.Exercises.Any(e => e.Id == exerciseId))
            .ToListAsync();

        return exerciseTypes
            .Select(et => new DropdownReturnDto { Value = et.Id, Label = et.ExerciseTypeName })
            .ToList();
    }
}
