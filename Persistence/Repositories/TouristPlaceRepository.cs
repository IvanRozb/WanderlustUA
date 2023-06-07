using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class TouristPlaceRepository : ITouristPlaceRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public TouristPlaceRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<TouristPlace?>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.TouristPlaces.ToListAsync(cancellationToken);

        public async Task<TouristPlace?> GetByIdAsync(Guid touristPlaceId, CancellationToken cancellationToken = default) =>
            await _dbContext.TouristPlaces.FirstOrDefaultAsync(x => x != null && x.Id == touristPlaceId, cancellationToken);

        public void Insert(TouristPlace? touristPlace) => _dbContext.TouristPlaces.Add(touristPlace);

        public void Remove(TouristPlace? touristPlace) => _dbContext.TouristPlaces.Remove(touristPlace);
    }
}
