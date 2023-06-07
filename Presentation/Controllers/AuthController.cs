using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly string _passwordKey;
    private readonly string _tokenKey;

    public AuthController(IServiceManager serviceManager, IConfiguration configuration)
    {
        _serviceManager = serviceManager;
        _passwordKey = configuration.GetSection("AppSettings:PasswordKey").Value;
        _tokenKey = configuration.GetSection("AppSettings:TokenKey").Value;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
    {
        var authEntity = await _serviceManager.AuthService.Register(userForRegistration, _passwordKey);
        return CreatedAtAction("Register", new {email = authEntity.Email}, authEntity);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLogin)
    {
        return Ok(await _serviceManager.AuthService.Login(userForLogin, _passwordKey, _tokenKey));
    }

    [HttpGet("refreshToken")]
    public string RefreshToken()
    {
        var userId = _serviceManager.AuthService.RefreshToken(User);
        return AuthHelper.CreateToken(userId, _tokenKey);
    }
}