using Domain.Entities;

namespace Domain.Repositories;

public interface IRouteRepository
{
    Task<IEnumerable<Route>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Route> GetByIdAsync(Guid routeId, CancellationToken cancellationToken = default);

    void Insert(Route route);

    void Remove(Route route);
}