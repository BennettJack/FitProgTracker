using fpt_backend.Data.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace fpt_backend.Controllers;

[Microsoft.AspNetCore.Components.Route("[controller]")]
public class UserAccountController : Controller
{
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return CreatedAtAction("test", user);
    }
}