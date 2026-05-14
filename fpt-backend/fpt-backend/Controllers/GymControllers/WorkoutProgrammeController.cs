using System.Text.Json;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Dto;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutProgrammeController : Controller
{
    private readonly IWorkoutProgrammeService _workoutProgrammeService;

    public WorkoutProgrammeController(IWorkoutProgrammeService workoutProgrammeService)
    {
        _workoutProgrammeService = workoutProgrammeService;
    }

    [HttpPost("newWorkoutProgramme")]
    public async Task<ActionResult<WorkoutProgramme>> AddNewWorkoutProgramme(
        [FromBody] WorkoutProgrammeCreateRequest programme
    )
    {
        Console.WriteLine(JsonSerializer.Serialize(programme));
        return Ok(await _workoutProgrammeService.AddAsync(programme));
    }

    [HttpGet("getWorkoutProgramme")]
    [Authorize(Roles = "standardUser")]
    public async Task<ActionResult<WorkoutProgramme>> GetById(int Id)
    {
        var programme = await _workoutProgrammeService.GetAsDtoAsync(Id);
        return Ok(programme);
    }

    [HttpPost("updateWorkoutProgramme")]
    public async Task<ActionResult<WorkoutProgrammeReturnDto>> Update(
        [FromBody] WorkoutProgrammeCreateRequest programme
    )
    {
        var ret = await _workoutProgrammeService.UpdateTestAsync(programme);
        return Ok(ret);
    }

    [HttpPost("createFiveThreeOneProgramme")]
    [Authorize(Roles = "standardUser")]
    public async Task<ActionResult<WorkoutProgrammeReturnDto>> CreateFiveThreeOneProgramme()
    {
        var test = User.GetUserId();
        var res = await _workoutProgrammeService.CreateProgrammeFromTemplate(2);
        return Ok(res);
    }
}
