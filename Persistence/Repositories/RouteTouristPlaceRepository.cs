using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RouteTouristPlaceRepository : IRouteTouristPlaceRepository
{
    private readonly RepositoryDbContext _dbContext;
    public RouteTouristPlaceRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<IEnumerable<RouteTouristPlace>> GetAllByRouteIdAsync(Guid routeId, CancellationToken cancellationToken = default) =>
        await _dbContext.RouteTouristPlaces.Where(x => x.RouteId == routeId).ToListAsync(cancellationToken);

    public async Task<RouteTouristPlace> GetByIdAsync(Guid routeTouristPlaceId, CancellationToken cancellationToken = default)
        => await _dbContext.RouteTouristPlaces.FirstOrDefaultAsync(x => x.Id == routeTouristPlaceId, cancellationToken);
    
    public void Insert(RouteTouristPlace routeTouristPlace) => _dbContext.RouteTouristPlaces.Add(routeTouristPlace);

    public void Remove(RouteTouristPlace routeTouristPlace) => _dbContext.RouteTouristPlaces.Remove(routeTouristPlace);
}