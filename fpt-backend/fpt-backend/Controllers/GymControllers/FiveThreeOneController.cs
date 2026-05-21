using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[ApiController]
[Route("api/[controller]")]
public class FiveThreeOneController : Controller
{
    private readonly IFiveThreeOneService _fiveThreeOneService;
    private readonly ICurrentUserService _currentUserService;

    public FiveThreeOneController(
        IFiveThreeOneService fiveThreeOneService,
        ICurrentUserService currentUserService
    )
    {
        _fiveThreeOneService = fiveThreeOneService;
        _currentUserService = currentUserService;
    }

    [HttpGet("get")]
    public async Task<ActionResult<FiveThreeOneController>> Update()
    {
        return Ok(await _fiveThreeOneService.GetByUserIdAsync(_currentUserService.UserId));
    }
}
