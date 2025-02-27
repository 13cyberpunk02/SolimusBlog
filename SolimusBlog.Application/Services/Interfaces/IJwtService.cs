using System.Security.Claims;
using SolimusBlog.Domain.Entities;

namespace SolimusBlog.Application.Services.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(SolimusUser user, string[] roles);
    string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}