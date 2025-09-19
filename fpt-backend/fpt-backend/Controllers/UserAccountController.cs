using fpt_backend.Data.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace fpt_backend.Controllers;

[Route("[controller]")]
public class UserAccountController : Controller
{
    
    [HttpGet("SignUp")]
    public async Task<IActionResult> SignUp()
    {
        var enrollmentFlowUrl = "https://auth.bennettj.uk/if/flow/default-enrollment-flow/"
                                + "?next=" + Uri.EscapeDataString("https://192.168.1.205/signin-oidc");
        
        return Redirect(enrollmentFlowUrl);
    }
}