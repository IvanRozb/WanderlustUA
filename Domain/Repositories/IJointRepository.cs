using Domain.Entities;

namespace Domain.Repositories;

public interface IJointRepository
{
    Task<IEnumerable<Joint>> GetAllByRouteIdAsync(Guid routeId, CancellationToken cancellationToken = default);

    Task<Joint> GetByIdAsync(Guid jointId, CancellationToken cancellationToken = default);

    void Insert(Joint joint);

    void Remove(Joint joint);
}