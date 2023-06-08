using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/routes")]
public class RoutesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public RoutesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetRoutes()
    {
        var routesDto = await _serviceManager.RouteService
            .GetAllByUserIdAsync(Guid.Parse(User.FindFirst("userId").Value));

        return Ok(routesDto);
    }
    
    [HttpGet("{routeId:guid}")]
    public async Task<IActionResult> GetRouteById(Guid routeId, CancellationToken cancellationToken)
    {
        var routeDto = await _serviceManager.RouteService
            .GetByIdAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, cancellationToken);

        return Ok(routeDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoute([FromBody] RouteForCreationDto routeForCreationDto, CancellationToken cancellationToken)
    {
        var response = await _serviceManager.RouteService
            .CreateAsync(Guid.Parse(User.FindFirst("userId").Value), routeForCreationDto, cancellationToken);

        return CreatedAtAction(nameof(GetRouteById), new { userId = Guid.Parse(User.FindFirst("userId").Value), routeId = response.Id }, response);
    }

    [HttpDelete("{routeId:guid}")]
    public async Task<IActionResult> DeleteRoute(Guid routeId, CancellationToken cancellationToken)
    {
        await _serviceManager.RouteService
            .DeleteAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, cancellationToken);

        return NoContent();
    }
}