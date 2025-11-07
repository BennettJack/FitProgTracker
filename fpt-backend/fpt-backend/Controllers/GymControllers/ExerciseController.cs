using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseController : Controller
{
    private readonly ExerciseService _exerciseService;

    public ExerciseController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpPost("AddExercise")]
    public async Task<IActionResult> AddExercise([FromBody] AddExerciseRequestDto exerciseDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var res = await _exerciseService.AddExercise(exerciseDto, User.Identity.Name);

        return res.Status switch
        {
            //TODO - Make a good return DTO. This the created at action res.data will cause cycle errors
            OperationStatus.Success => CreatedAtAction(nameof(AddExercise), new { id = res.Data!.ExerciseId }, "created"),
            OperationStatus.BadRequest => BadRequest(new { error = res.Message }),
            OperationStatus.NotFound => NotFound(new { error = res.Message }),
            OperationStatus.Error => StatusCode(500, new { error = res.Message }),
            _ => StatusCode(500, new { error = "Internal server error" })
        };
    }
}