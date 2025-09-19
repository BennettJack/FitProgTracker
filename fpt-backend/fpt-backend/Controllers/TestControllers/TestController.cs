using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.TestControllers;
[Route("[controller]")]
public class TestController : Controller
{
    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }

    [HttpGet("test2")]
    public IActionResult Test2()
    {
        return Ok();
    }
}