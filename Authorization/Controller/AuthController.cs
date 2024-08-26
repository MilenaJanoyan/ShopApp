using Microsoft.AspNetCore.Mvc;
using ShopApp.Authorization.Entity;
using ShopApp.Authorization.Models;
using ShopApp.Authorization.Service.Interfaces;

namespace ShopApp.Authorization.Controller;

/// <summary>
/// Controller for managing auth.
/// </summary>
//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
    {
        var result = await _authService.SignUp(signUpModel.Username, signUpModel.Password, signUpModel.Phone);
        if (!result)
        {
            return Conflict("Username already exists");
        }

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var tokenResponse = await _authService.Login(loginModel.Username, loginModel.Password);
        if (tokenResponse == null)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(tokenResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] Token tokenModel)
    {
        var tokenResponse = await _authService.RefreshToken(tokenModel.RefreshToken);
        if (tokenResponse == null)
        {
            return Unauthorized("Invalid refresh token");
        }

        return Ok(tokenResponse);
    }
}
