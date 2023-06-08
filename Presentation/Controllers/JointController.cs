using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/routes/{routeId:guid}/joints")]
public class JointController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public JointController(IServiceManager serviceManager) => _serviceManager = serviceManager;
    
    [HttpGet]
    public async Task<IActionResult> GetJoints(Guid userId, Guid routeId, CancellationToken cancellationToken)
    {
        var jointsDto = await _serviceManager.JointService
            .GetAllByUserRouteIdAsync(userId, routeId, cancellationToken);

        return Ok(jointsDto);
    }

    [HttpGet("{jointId:guid}")]
    public async Task<IActionResult> GetJointById(Guid userId, Guid routeId, Guid touristPlaceId, Guid jointId,  CancellationToken cancellationToken)
    { 
        var jointDto = await _serviceManager.JointService
            .GetByIdAsync(userId, routeId, jointId, cancellationToken);

        return Ok(jointDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateJoint(Guid userId, Guid routeId, Guid touristPlaceId, 
        [FromBody] JointForCreationDto routeForCreationDto, CancellationToken cancellationToken = default)
    {
        var response = await _serviceManager.JointService
            .CreateAsync(userId, routeId, touristPlaceId, routeForCreationDto, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{jointId:guid}")]
    public async Task<IActionResult> DeleteJoint(Guid userId, Guid routeId, Guid jointId, CancellationToken cancellationToken)
    {
        await _serviceManager.JointService
            .DeleteAsync(userId, routeId, jointId, cancellationToken);

        return NoContent();
    }
}