using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/routes/{routeId:guid}/joints")]
public class JointController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public JointController(IServiceManager serviceManager) => _serviceManager = serviceManager;
    
    [HttpGet]
    public async Task<IActionResult> GetJoints(Guid routeId, CancellationToken cancellationToken)
    {
        var jointsDto = await _serviceManager.JointService
            .GetAllByUserRouteIdAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, cancellationToken);

        return Ok(jointsDto);
    }

    [HttpGet("{jointId:guid}")]
    public async Task<IActionResult> GetJointById(Guid routeId, Guid jointId,  CancellationToken cancellationToken)
    { 
        var jointDto = await _serviceManager.JointService
            .GetByIdAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, jointId, cancellationToken);

        return Ok(jointDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateJoint(Guid routeId, Guid touristPlaceId, 
        [FromBody] JointForCreationDto routeForCreationDto, CancellationToken cancellationToken = default)
    {
        var response = await _serviceManager.JointService
            .CreateAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, touristPlaceId, routeForCreationDto, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{jointId:guid}")]
    public async Task<IActionResult> DeleteJoint(Guid routeId, Guid jointId, CancellationToken cancellationToken)
    {
        await _serviceManager.JointService
            .DeleteAsync(Guid.Parse(User.FindFirst("userId").Value), routeId, jointId, cancellationToken);

        return NoContent();
    }
}