using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/routes/{routeId:guid}/route-tourist-places")]
public class RouteTouristPlaceController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public RouteTouristPlaceController(IServiceManager serviceManager) => _serviceManager = serviceManager;
    
    [HttpGet]
    public async Task<IActionResult> GetRouteTouristPlaces(Guid userId, Guid routeId, CancellationToken cancellationToken)
    {
        var routeTouristPlacesDto = await _serviceManager.RouteTouristPlaceService
            .GetAllByUserRouteIdAsync(userId, routeId, cancellationToken);

        return Ok(routeTouristPlacesDto);
    }

    [HttpGet("{routeTouristPlaceId:guid}")]
    public async Task<IActionResult> GetRouteTouristPlaceById(Guid userId, Guid routeId, Guid touristPlaceId, Guid routeTouristPlaceId,  CancellationToken cancellationToken)
    { 
        var routeTouristPlaceDto = await _serviceManager.RouteTouristPlaceService
            .GetByIdAsync(userId, routeId, routeTouristPlaceId, cancellationToken);

        return Ok(routeTouristPlaceDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRouteTouristPlace(Guid userId, Guid routeId, Guid touristPlaceId, 
        [FromBody] RouteTouristPlaceForCreationDto routeForCreationDto, CancellationToken cancellationToken = default)
    {
        var response = await _serviceManager.RouteTouristPlaceService
            .CreateAsync(userId, routeId, touristPlaceId, routeForCreationDto, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{routeTouristPlaceId:guid}")]
    public async Task<IActionResult> DeleteRouteTouristPlace(Guid userId, Guid routeId, Guid routeTouristPlaceId, CancellationToken cancellationToken)
    {
        await _serviceManager.RouteTouristPlaceService
            .DeleteAsync(userId, routeId, routeTouristPlaceId, cancellationToken);

        return NoContent();
    }
}