using ShopApp.Users.Entity;
using ShopApp.Authorization.Entity;
using ShopApp.Authorization.Models;
using ShopApp.Users.Repository.Interface;
using ShopApp.Authorization.Service.Interfaces;
using ShopApp.Authorization.Repository.Interface;

namespace ShopApp.Authorization.Service;

/// <summary>
/// Controller for managing authorization.
/// </summary>
public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenRepository _tokenRepository;

    public AuthService(IUserRepository userRepository, ITokenRepository tokenRepository, IPasswordService passwordService, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _passwordService = passwordService;
    }

    public async Task<bool> SignUp(string username, string password, string phone)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(username);
        if (existingUser != null)
        {
            return false; // Username already exists
        }

        var salt = _passwordService.GenerateSalt();
        var hashedPassword = _passwordService.HashPassword(password, salt);

        var user = new User
        {
            Username = username,
            Password = hashedPassword,
            Salt = salt,
            Phone = phone
        };

        await _userRepository.AddUserAsync(user);

        return true;
    }

    public async Task<TokenResponse> Login(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || !_passwordService.VerifyPassword(password, user.Password, user.Salt))
        {
            return null; // Invalid username or password
        }

        var accessToken = _tokenService.GenerateAccessToken(user.Username);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var token = new Token
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7) // Example expiry date
        };

        await _tokenRepository.AddTokenAsync(token);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenResponse> RefreshToken(string refreshToken)
    {
        var token = await _tokenRepository.GetTokenByRefreshTokenAsync(refreshToken);
        if (token == null)
        {
            return null; // Invalid refresh token
        }

        var user = await _userRepository.GetUserByIdAsync(token.UserId);
        var newAccessToken = _tokenService.GenerateAccessToken(user.Username);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        token.AccessToken = newAccessToken;
        token.RefreshToken = newRefreshToken;
        token.ExpiryDate = DateTime.UtcNow.AddDays(7); // Example expiry date

        await _tokenRepository.UpdateTokenAsync(token);

        return new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
