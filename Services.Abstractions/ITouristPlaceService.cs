using Contracts;

namespace Services.Abstractions;

public interface ITouristPlaceService
{
    Task<IEnumerable<TouristPlaceDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TouristPlaceDto> GetByIdAsync(Guid touristPlaceId, CancellationToken cancellationToken = default);

    Task<TouristPlaceDto> CreateAsync(TouristPlaceForCreationDto touristPlaceForCreationDto, CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid touristPlaceId, TouristPlaceForUpdateDto touristPlaceForUpdateDto, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid touristPlaceId, CancellationToken cancellationToken = default);
}