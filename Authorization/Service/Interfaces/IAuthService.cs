using ShopApp.Authorization.Models;

namespace ShopApp.Authorization.Service.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignUp(string username, string password, string phone);

        Task<TokenResponse> RefreshToken(string refreshToken);

        Task<TokenResponse> Login(string username, string password);
    }
}
