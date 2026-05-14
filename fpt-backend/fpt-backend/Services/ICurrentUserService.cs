using System.Security.Claims;

namespace fpt_backend.Services.GymServices.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    ClaimsPrincipal User { get; }
}