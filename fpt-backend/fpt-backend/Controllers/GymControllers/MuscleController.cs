using fpt_backend.Services.GymServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class MuscleController : Controller
{
    private readonly MuscleService _muscleService;

    public MuscleController(MuscleService muscleService)
    {
        _muscleService = muscleService;
    }

    [HttpGet("getOptionData")]
    public async Task<IActionResult> GetOptionData()
    {
        var res = await _muscleService.GetMuscleListAsDropdown();

        return res.Status switch
        {
            ResultStatus.Success => Ok(res),
            ResultStatus.BadRequest => BadRequest(new { error = res.Message }),
            ResultStatus.NotFound => NotFound(new { error = res.Message }),
            ResultStatus.Error => StatusCode(500, new { error = res.Message }),
            _ => StatusCode(500, new { error = "Internal server error" })
        };
    }
}