using System.Text.Json;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class WorkoutProgrammeController : Controller
{
    private readonly IWorkoutProgrammeService _workoutProgrammeService;

    public WorkoutProgrammeController(IWorkoutProgrammeService workoutProgrammeService)
    {
        _workoutProgrammeService = workoutProgrammeService;
    }

    [HttpPost("newWorkoutProgramme")]
    public async Task<ActionResult<WorkoutProgramme>> 
        AddNewWorkoutProgramme([FromBody] WorkoutProgrammeCreateRequest programme)
    {
        Console.WriteLine(JsonSerializer.Serialize(programme));
        return Ok(await _workoutProgrammeService.AddAsync(programme));
    }
    
    [HttpGet("getWorkoutProgramme")]
    public async Task<ActionResult<WorkoutProgramme>>GetById(int Id)
    {
        Console.WriteLine(Id);
        var programme = await _workoutProgrammeService.GetAsDtoAsync(Id);
        return Ok(programme);
    }
}