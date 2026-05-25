using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseController : Controller
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpPost("AddExercise")]
    public async Task<IActionResult> AddExercise([FromBody] AddExerciseRequestDto exerciseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var res = await _exerciseService.AddAsync(exerciseDto, "System");

        return Ok();
    }

    [HttpGet("ExerciseOptionData")]
    public async Task<IActionResult> GetOptionData()
    {
        var res = await _exerciseService.GetExerciseOptionsAsync();

        return Ok(res);
    }

    [HttpGet("GetExercises")]
    public async Task<IActionResult> GetExercises()
    {
        var res = await _exerciseService.GetListAsDropdownAsync();
        return Ok(res);
    }
}
