using IdentityService.API.Models;

namespace IdentityService.API.Services;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
}