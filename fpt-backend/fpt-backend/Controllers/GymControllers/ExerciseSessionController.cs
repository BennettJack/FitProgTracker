using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseSessionController : Controller
{
    //remove this after testing
    private readonly IExerciseRepository _repository;
    public ExerciseSessionController(IExerciseRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetExerciseSession")]
    public async Task<IActionResult> GetExerciseSession([FromQuery] int id)
    {
        var exercises = await _repository.GetAllAsync();
        var test = exercises.Data;
        var sets = new List<ExerciseSet>()
        {
            new(){ Exercise = test[1], ExerciseSetId = 1, RepCeiling = 10, RepFloor = 4},
            new(){ Exercise = test[1], ExerciseSetId = 2, RepCeiling = 550, RepFloor = 99},
            new(){ Exercise = test[2], ExerciseSetId = 3, RepCeiling = 110, RepFloor = 44},
            new(){ Exercise = test[2], ExerciseSetId = 4, RepCeiling = 96, RepFloor = 45},
            new(){ Exercise = test[2], ExerciseSetId = 5, RepCeiling = 16, RepFloor = 46},
            new(){ Exercise = test[3], ExerciseSetId = 6, RepCeiling = 48, RepFloor = 47},
            new(){ Exercise = test[3], ExerciseSetId = 7, RepCeiling = 222, RepFloor = 48},
            new(){ Exercise = test[4], ExerciseSetId = 8, RepCeiling = 143, RepFloor = 49},
        };
        var tempRes = new SessionBuilderDTO();

        tempRes.SessionId = 1;
        tempRes.SessionName = "Exercise Session Test";

        var setList1 = new List<ExerciseSet>();
        var setList2 = new List<ExerciseSet>();
        var setList3 = new List<ExerciseSet>();
        var setList4 = new List<ExerciseSet>();
        
        setList1.AddRange(sets[0], sets[1]);
        setList2.AddRange(sets[2], sets[3], sets[4]);
        setList3.AddRange(sets[5], sets[6]);
        setList4.Add(sets[7]);
        
        return Ok(tempRes);
    }
}