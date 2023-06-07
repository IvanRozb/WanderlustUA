using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

internal sealed class TouristPlaceService : ITouristPlaceService
{
    private readonly IRepositoryManager _repositoryManager;
    public TouristPlaceService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<TouristPlaceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var touristPlaces = await _repositoryManager.TouristPlaceRepository.GetAllAsync(cancellationToken);

        var touristPlacesDto = touristPlaces.Adapt<IEnumerable<TouristPlaceDto>>();

        return touristPlacesDto;
    }

    public async Task<IEnumerable<TouristPlaceDto?>> GetBySearchAsync(string searchParam, CancellationToken cancellationToken = default)
    {
        var touristPlaces = await _repositoryManager.TouristPlaceRepository.GetBySearchAsync(searchParam, cancellationToken);

        var touristPlacesDto = touristPlaces.Adapt<IEnumerable<TouristPlaceDto>>();

        return touristPlacesDto;
    }

    public async Task<TouristPlaceDto> GetByIdAsync(Guid touristPlaceId, CancellationToken cancellationToken = default)
    {
        var touristPlace = await _repositoryManager.TouristPlaceRepository.GetByIdAsync(touristPlaceId, cancellationToken);
        if (touristPlace is null)
        {
            throw new TouristPlaceNotFoundException(touristPlaceId);
        }
        var touristPlaceDto = touristPlace.Adapt<TouristPlaceDto>();
        return touristPlaceDto;
    }
    public async Task<TouristPlaceDto> CreateAsync(TouristPlaceForCreationDto touristPlaceForCreationDto, CancellationToken cancellationToken = default)
    {
        var touristPlace = touristPlaceForCreationDto.Adapt<TouristPlace>();
        _repositoryManager.TouristPlaceRepository.Insert(touristPlace);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return touristPlace.Adapt<TouristPlaceDto>();
    }
    public async Task UpdateAsync(Guid touristPlaceId, TouristPlaceForUpdateDto touristPlaceForUpdateDto, CancellationToken cancellationToken = default)
    {
        var touristPlace = await _repositoryManager.TouristPlaceRepository.GetByIdAsync(touristPlaceId, cancellationToken);
        if (touristPlace is null)
        {
            throw new TouristPlaceNotFoundException(touristPlaceId);
        }
        touristPlace.Name = touristPlaceForUpdateDto.Name;
        touristPlace.Category = touristPlaceForUpdateDto.Category;
        touristPlace.Region = touristPlaceForUpdateDto.Region;
        touristPlace.Description = touristPlaceForUpdateDto.Description;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid touristPlaceId, CancellationToken cancellationToken = default)
    {
        var touristPlace = await _repositoryManager.TouristPlaceRepository.GetByIdAsync(touristPlaceId, cancellationToken);
        if (touristPlace is null)
        {
            throw new TouristPlaceNotFoundException(touristPlaceId);
        }
        _repositoryManager.TouristPlaceRepository.Remove(touristPlace);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}