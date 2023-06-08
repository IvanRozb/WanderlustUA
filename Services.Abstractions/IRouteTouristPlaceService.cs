using Contracts;

namespace Services.Abstractions;

public interface IRouteTouristPlaceService
{
    Task<IEnumerable<RouteTouristPlaceDto>> GetAllByUserRouteIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default);
    Task<RouteTouristPlaceDto> GetByIdAsync(Guid userId, Guid routeId, Guid routeTouristPlaceId, CancellationToken cancellationToken = default);
    Task<RouteTouristPlaceDto> CreateAsync(Guid userId, Guid routeId, Guid touristPlaceId,
        RouteTouristPlaceForCreationDto routeForCreationDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid userId, Guid routeId, Guid routeTouristPlaceId, CancellationToken cancellationToken = default);
}