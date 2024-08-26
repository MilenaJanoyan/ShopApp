using System.Security.Claims;

namespace ShopApp.Authorization.Service.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string username);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
