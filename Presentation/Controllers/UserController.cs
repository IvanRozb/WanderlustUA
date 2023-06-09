using System.Security.Claims;
using Contracts;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UsersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        var users = await _serviceManager.UserService.GetAllAsync(cancellationToken);

        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        var userDto = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto userForCreationDto)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        var userDto = await _serviceManager.UserService.CreateAsync(userForCreationDto);

        return CreatedAtAction(nameof(GetUserById), new { userId = userDto.Id }, userDto);
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserForUpdateDto userForUpdateDto, CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        await _serviceManager.UserService.UpdateAsync(userId, userForUpdateDto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        await _serviceManager.UserService.DeleteAsync(userId, cancellationToken);

        return NoContent();
    }
}