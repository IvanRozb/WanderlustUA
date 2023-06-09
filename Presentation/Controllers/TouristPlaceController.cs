using System.Security.Claims;
using Contracts;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/tourist-place")]
public class TouristPlaceController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TouristPlaceController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetTouristPlaces(CancellationToken cancellationToken)
    {
        var touristPlaces = await _serviceManager.TouristPlaceService.GetAllAsync(cancellationToken);

        return Ok(touristPlaces);
    }
    
    [HttpGet("{searchParam}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTouristPlacesBySearch(string searchParam, CancellationToken cancellationToken)
    {
        var touristPlaces = await _serviceManager.TouristPlaceService.GetBySearchAsync(searchParam, cancellationToken);

        return Ok(touristPlaces);
    }

    [HttpGet("{touristPlaceId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTouristPlaceById(Guid touristPlaceId, CancellationToken cancellationToken)
    {
        var touristPlaceDto = await _serviceManager.TouristPlaceService.GetByIdAsync(touristPlaceId, cancellationToken);

        return Ok(touristPlaceDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristPlace([FromBody] TouristPlaceForCreationDto touristPlaceForCreationDto)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        var touristPlaceDto = await _serviceManager.TouristPlaceService.CreateAsync(touristPlaceForCreationDto);

        return CreatedAtAction(nameof(GetTouristPlaceById), new { touristPlaceId = touristPlaceDto.Id }, touristPlaceDto);
    }

    [HttpPut("{touristPlaceId:guid}")]
    public async Task<IActionResult> UpdateTouristPlace(Guid touristPlaceId, [FromBody] TouristPlaceForUpdateDto touristPlaceForUpdateDto, CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        await _serviceManager.TouristPlaceService.UpdateAsync(touristPlaceId, touristPlaceForUpdateDto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{touristPlaceId:guid}")]
    public async Task<IActionResult> DeleteTouristPlace(Guid touristPlaceId, CancellationToken cancellationToken)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
            throw new InsufficientPrivilegeException("admin");
        
        await _serviceManager.TouristPlaceService.DeleteAsync(touristPlaceId, cancellationToken);

        return NoContent();
    }
}