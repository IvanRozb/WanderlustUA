using Contracts;

namespace Services.Abstractions;

public interface IJointService
{
    Task<IEnumerable<JointDto>> GetAllByUserRouteIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default);
    Task<JointDto> GetByIdAsync(Guid userId, Guid routeId, Guid jointId, CancellationToken cancellationToken = default);
    Task<JointDto> CreateAsync(Guid userId, Guid routeId, Guid touristPlaceId,
        JointForCreationDto routeForCreationDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid userId, Guid routeId, Guid jointId, CancellationToken cancellationToken = default);
}