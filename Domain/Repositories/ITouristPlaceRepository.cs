using Domain.Entities;

namespace Domain.Repositories;

public interface ITouristPlaceRepository
{
    Task<IEnumerable<TouristPlace?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TouristPlace?>> GetBySearchAsync(string searchParam, CancellationToken cancellationToken = default);
    Task<TouristPlace?> GetByIdAsync(Guid touristPlaceId, CancellationToken cancellationToken = default);
    void Insert(TouristPlace? touristPlace);
    void Remove(TouristPlace? touristPlace); 
    
}