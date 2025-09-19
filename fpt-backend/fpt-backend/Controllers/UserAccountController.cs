using System.Diagnostics;
using fpt_backend.Data.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace fpt_backend.Controllers;

[Route("[controller]")]
public class UserAccountController : Controller
{
    [HttpGet("SignUp")]
    public IActionResult Signup()
    {
        // Enrollment flow URL
        var enrollmentFlowUrl =
            "https://auth.bennettj.uk/if/flow/default-enrollment-flow/" +
            "?next=/application/o/fitnessprogresstracker/";

        return Redirect(enrollmentFlowUrl);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        // Normal OIDC login challenge
        return Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:3000" },
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
    
    [HttpGet("checkcookie")]
    public IActionResult CheckCookie()
    {
        var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
        return Ok(new
        {
            Authenticated = isAuthenticated,
            Username = User.Identity?.Name
        });
    }

    [Authorize]
    [HttpGet("testAuth")]
    public IActionResult TestAuth()
    {
        Debug.WriteLine("TestAuth");
        return Ok();
    }
}