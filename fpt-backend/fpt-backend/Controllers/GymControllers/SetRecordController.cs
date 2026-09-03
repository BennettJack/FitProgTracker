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

    [HttpGet("GetTodaysRecords/{sessionId:int}")]
    public async Task<IActionResult> GetTodayRecords(int sessionId)
    {
        var res = await _exerciseSetRecordService.GetTodayRecordAsync(sessionId);
        return Ok(res);
    }

    [HttpPost("GetMostRecentRecords")]
    public async Task<IActionResult> GetMostRecentRecords([FromBody] List<int> exerciseIds)
    {
        if (exerciseIds.Count == 0)
            return BadRequest();
        var res = await _exerciseSetRecordService.GetMostRecentRecordsAsync(exerciseIds);
        return Ok(res);
    }

    [HttpPost("UpdateSetRecord")]
    public async Task<IActionResult> UpdateSetRecord(
        [FromBody] ExerciseSetRecordCreateRequest request
    )
    {
        var res = await _exerciseSetRecordService.UpdateAsync(request);
        if (res == null)
            return NotFound();
        return Ok(res);
    }
}
