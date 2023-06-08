using Domain.Entities;

namespace Domain.Repositories;

public interface IRouteTouristPlaceRepository
{
    Task<IEnumerable<RouteTouristPlace>> GetAllByRouteIdAsync(Guid routeId, CancellationToken cancellationToken = default);

    Task<RouteTouristPlace> GetByIdAsync(Guid routeTouristPlaceId, CancellationToken cancellationToken = default);

    void Insert(RouteTouristPlace routeTouristPlace);

    void Remove(RouteTouristPlace routeTouristPlace);
}