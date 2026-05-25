using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseTypeController : Controller
{
    private readonly IExerciseTypeService _exerciseTypeService;

    public ExerciseTypeController(IExerciseTypeService exerciseTypeService)
    {
        _exerciseTypeService = exerciseTypeService;
    }

    [HttpGet("GetExerciseTypes")]
    public async Task<IActionResult> GetExerciseTypes()
    {
        var res = await _exerciseTypeService.GetListAsDropdownAsync();
        return Ok(res);
    }
}
