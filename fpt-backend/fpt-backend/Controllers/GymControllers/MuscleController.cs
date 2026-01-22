using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class MuscleController : Controller
{
    private readonly IMuscleService _muscleService;

    public MuscleController(IMuscleService muscleService)
    {
        _muscleService = muscleService;
    }

    [HttpGet("getOptionData")]
    public async Task<IActionResult> GetOptionData()
    {
        var res = await _muscleService.GetListAsDropdownAsync();

        return Ok(res);
    }
}