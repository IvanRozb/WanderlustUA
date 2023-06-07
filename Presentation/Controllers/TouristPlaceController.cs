using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/tourist-place")]
public class TouristPlaceController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TouristPlaceController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetTouristPlaces(CancellationToken cancellationToken)
    {
        var touristPlaces = await _serviceManager.TouristPlaceService.GetAllAsync(cancellationToken);

        return Ok(touristPlaces);
    }
    
    [HttpGet("{searchParam}")]
    public async Task<IActionResult> GetTouristPlacesBySearch(string searchParam, CancellationToken cancellationToken)
    {
        var touristPlaces = await _serviceManager.TouristPlaceService.GetBySearchAsync(searchParam, cancellationToken);

        return Ok(touristPlaces);
    }

    [HttpGet("{touristPlaceId:guid}")]
    public async Task<IActionResult> GetTouristPlaceById(Guid touristPlaceId, CancellationToken cancellationToken)
    {
        var touristPlaceDto = await _serviceManager.TouristPlaceService.GetByIdAsync(touristPlaceId, cancellationToken);

        return Ok(touristPlaceDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristPlace([FromBody] TouristPlaceForCreationDto touristPlaceForCreationDto)
    {
        var touristPlaceDto = await _serviceManager.TouristPlaceService.CreateAsync(touristPlaceForCreationDto);

        return CreatedAtAction(nameof(GetTouristPlaceById), new { touristPlaceId = touristPlaceDto.Id }, touristPlaceDto);
    }

    [HttpPut("{touristPlaceId:guid}")]
    public async Task<IActionResult> UpdateTouristPlace(Guid touristPlaceId, [FromBody] TouristPlaceForUpdateDto touristPlaceForUpdateDto, CancellationToken cancellationToken)
    {
        await _serviceManager.TouristPlaceService.UpdateAsync(touristPlaceId, touristPlaceForUpdateDto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{touristPlaceId:guid}")]
    public async Task<IActionResult> DeleteTouristPlace(Guid touristPlaceId, CancellationToken cancellationToken)
    {
        await _serviceManager.TouristPlaceService.DeleteAsync(touristPlaceId, cancellationToken);

        return NoContent();
    }
}