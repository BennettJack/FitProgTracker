using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseSessionController : Controller
{
    private readonly IExerciseSessionService _exerciseSessionService;
    public ExerciseSessionController(IExerciseSessionService exerciseSessionService)
    {
        _exerciseSessionService = exerciseSessionService;
    }

    [HttpGet("GetExerciseSession")]
    public async Task<IActionResult> GetExerciseSession([FromQuery] int id)
    {
        throw new NotImplementedException();
    }
}