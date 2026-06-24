using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class SetRecordController : Controller
{
    private readonly IExerciseSetRecordService _exerciseSetRecordService;

    public SetRecordController(IExerciseSetRecordService exerciseSetRecordService)
    {
        _exerciseSetRecordService = exerciseSetRecordService;
    }

    [HttpPost("AddSetRecord")]
    public async Task<IActionResult> AddSetRecord([FromBody] ExerciseSetRecordCreateRequest request)
    {
        var res = await _exerciseSetRecordService.AddAsync(request);
        return Ok(res);
    }
}
