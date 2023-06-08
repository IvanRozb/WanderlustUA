using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/users/{userId:guid}/routes")]
public class RoutesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public RoutesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetRoutes(Guid userId)
    {
        throw new Exception("fwaawfawfawf");
        var routesDto = await _serviceManager.RouteService
            .GetAllByUserIdAsync(Guid.Parse(User.FindFirst("userId").Value));

        return Ok(routesDto);
    }
    
    [HttpGet("{routeId:guid}")]
    public async Task<IActionResult> GetRouteById(Guid userId, Guid routeId, CancellationToken cancellationToken)
    {
        var routeDto = await _serviceManager.RouteService.GetByIdAsync(userId, routeId, cancellationToken);

        return Ok(routeDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoute(Guid userId, [FromBody] RouteForCreationDto routeForCreationDto, CancellationToken cancellationToken)
    {
        var response = await _serviceManager.RouteService.CreateAsync(userId, routeForCreationDto, cancellationToken);

        return CreatedAtAction(nameof(GetRouteById), new { userId = response.UserId, routeId = response.Id }, response);
    }

    [HttpDelete("{routeId:guid}")]
    public async Task<IActionResult> DeleteRoute(Guid userId, Guid routeId, CancellationToken cancellationToken)
    {
        await _serviceManager.RouteService.DeleteAsync(userId, routeId, cancellationToken);

        return NoContent();
    }
}