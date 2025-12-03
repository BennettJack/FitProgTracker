using fpt_backend.Data.DTO.GymDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.GymRepositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class ExerciseSessionController : Controller
{
    //remove this after testing
    private readonly IExerciseRepository _repository;
    public ExerciseSessionController(IExerciseRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetExerciseSession")]
    public async Task<IActionResult> GetExerciseSession([FromQuery] int id)
    {
        throw new NotImplementedException();
    }
}