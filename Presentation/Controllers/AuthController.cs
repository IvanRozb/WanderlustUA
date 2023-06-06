using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly IConfiguration _configuration;

    public AuthController(IServiceManager serviceManager, IConfiguration configuration)
    {
        _serviceManager = serviceManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
    {
        _serviceManager.AuthService.Register(userForRegistration, _configuration
            .GetSection("AppSettings:PasswordKey").Value);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLogin)
    {
        return Ok();
    }
}