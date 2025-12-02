using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class SetController : Controller
{
    private readonly IExerciseSetService _setService;

    public SetController(IExerciseSetService setService)
    {
        _setService = setService;
    }

    [HttpPost("/Add")]
    
    public async Task<IActionResult> AddSet([FromBody] ExerciseSet set)
    {
        var res = await _setService.AddAsync(set);
        if (res.IsSuccess)
        {
            return Ok(res.Data);
        }
        return BadRequest(res.Message);
    }

    [HttpPost("/AddMultiple")]
    public async Task<IActionResult> AddMultipleSets([FromBody] List<ExerciseSet> sets)
    {
        var res = await _setService.AddMultipleAsync(sets);
        return Ok(res.Data);
    }
}