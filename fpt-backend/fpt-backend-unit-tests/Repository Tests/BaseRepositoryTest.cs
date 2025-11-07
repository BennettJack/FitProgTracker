using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend_unit_tests.Repository_Tests;

public class BaseRepositoryTest
{
    public List<Muscle> Muscles { get; set; } = new List<Muscle>
    {
        new() { MuscleId = 1, MuscleName = "Biceps"},
        new() { MuscleId = 2, MuscleName = "Triceps"},
        new() { MuscleId = 3, MuscleName = "Pectorals"},
        new() { MuscleId = 4, MuscleName = "Hamstrings"},
        new() { MuscleId = 9, MuscleName = "Calf"},
    };
    [Fact]
    public async Task BaseRepository_FindMuscleById_ReturnMuscle_ConcreteContext()
    {
        
        //Arrange
        var data = Muscles.AsQueryable();
        
        var contextOptions = new DbContextOptionsBuilder<FptDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        var context = new FptDbContext(contextOptions);
        context.Muscles.AddRange(data);
        await context.SaveChangesAsync();
        
        var baseRepository = new BaseRepository<Muscle>(context);
        //Act

        var res = baseRepository.GetByIdAsync(9).Result;
        //Assert
        Assert.NotNull(res);
        Assert.Equal(9, res.Data.MuscleId);
        Assert.Equal("Calf", res.Data.MuscleName);
    }
}