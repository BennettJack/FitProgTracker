using Microsoft.AspNetCore.Identity;

namespace fpt_backend.Data.Models.UserModels;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}