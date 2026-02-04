using fpt_backend.Data.DTO.GymDTOs;
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
    
    public async Task<IActionResult> AddSet([FromBody] ExerciseSetCreationDto set)
    {
        var res = await _setService.AddAsync(set);

        
        return Ok(res);

    }

    [HttpPost("/AddMultiple")]
    public async Task<IActionResult> AddMultipleSets([FromBody] List<Set> sets)
    {
        var res = await _setService.AddMultipleAsync(sets);
        return Ok(res);
    }
}