using Contracts;

namespace Services.Abstractions;

public interface IRouteService
{
    Task<IEnumerable<RouteDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<RouteDto> GetByIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken);

    Task<RouteDto> CreateAsync(Guid userId, RouteForCreationDto routeForCreationDto, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default);
}