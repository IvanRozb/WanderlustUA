using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

public class RouteTouristPlaceService : IRouteTouristPlaceService
{
    private readonly IRepositoryManager _repositoryManager;
    public RouteTouristPlaceService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<RouteTouristPlaceDto>> GetAllByUserRouteIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var route = await _repositoryManager.RouteRepository.GetByIdAsync(routeId, cancellationToken);
        if (route is null)
        {
            throw new RouteNotFoundException(routeId);
        }
        if (route.UserId != userId)
        {
            throw new RouteDoesNotBelongToUserException(userId, routeId);
        }

        var routeTouristPlaces = await _repositoryManager.RouteTouristPlaceRepository.GetAllByRouteIdAsync(routeId, cancellationToken);
        var routeTouristPlacesDto = routeTouristPlaces.Adapt<IEnumerable<RouteTouristPlaceDto>>();
        return routeTouristPlacesDto;
    }

    public async Task<RouteTouristPlaceDto> GetByIdAsync(Guid userId, Guid routeId, Guid touristPlaceId, Guid routeTouristPlaceId, CancellationToken cancellationToken)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var route = await _repositoryManager.RouteRepository.GetByIdAsync(routeId, cancellationToken);
        if (route is null)
        {
            throw new RouteNotFoundException(routeId);
        }
        if (route.UserId != userId)
        {
            throw new RouteDoesNotBelongToUserException(userId, routeId);
        }
        
        var touristPlace = await _repositoryManager.TouristPlaceRepository.GetByIdAsync(touristPlaceId, cancellationToken);
        if (touristPlace is null)
        {
            throw new TouristPlaceNotFoundException(touristPlaceId);
        }
        
        var routeTouristPlace = await _repositoryManager.RouteTouristPlaceRepository.GetByIdAsync(routeTouristPlaceId, cancellationToken);
        if (routeTouristPlace is null)
        {
            throw new RouteTouristPlaceNotFoundException(routeTouristPlaceId);
        }
        if (routeTouristPlace.RouteId != routeId)
        {
            throw new RouteTouristPlaceDoesNotBelongToRouteException(routeTouristPlaceId, routeId);
        }

        if (routeTouristPlace.TouristPlaceId != touristPlaceId)
        {
            throw new RouteTouristPlaceDoesNotBelongToTouristPlaceException(routeTouristPlaceId, touristPlaceId);
        }
        
        var routeTouristPlaceDto = routeTouristPlace.Adapt<RouteTouristPlaceDto>();
        return routeTouristPlaceDto;
    }

    public async Task<RouteTouristPlaceDto> CreateAsync(Guid userId, Guid routeId, Guid touristPlaceId, RouteTouristPlaceForCreatingDto routeTouristPlaceForCreationDto,
        CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var route = await _repositoryManager.RouteRepository.GetByIdAsync(routeId, cancellationToken);
        if (route is null)
        {
            throw new RouteNotFoundException(routeId);
        }
        if (route.UserId != userId)
        {
            throw new RouteDoesNotBelongToUserException(userId, routeId);
        }
        
        var touristPlace = await _repositoryManager.TouristPlaceRepository.GetByIdAsync(touristPlaceId, cancellationToken);
        if (touristPlace is null)
        {
            throw new TouristPlaceNotFoundException(touristPlaceId);
        }
        
        var routeTouristPlaceDto = routeTouristPlaceForCreationDto.Adapt<RouteTouristPlace>();
        routeTouristPlaceDto.RouteId = routeId;
        routeTouristPlaceDto.TouristPlaceId = touristPlaceId;
        _repositoryManager.RouteTouristPlaceRepository.Insert(routeTouristPlaceDto);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return routeTouristPlaceDto.Adapt<RouteTouristPlaceDto>();
    }

    public async Task DeleteAsync(Guid userId, Guid routeId, Guid routeTouristPlaceId, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var route = await _repositoryManager.RouteRepository.GetByIdAsync(routeId, cancellationToken);
        if (route is null)
        {
            throw new RouteNotFoundException(routeId);
        }
        if (route.UserId != userId)
        {
            throw new RouteDoesNotBelongToUserException(userId, routeId);
        }

        var routeTouristPlace =
            await _repositoryManager.RouteTouristPlaceRepository.GetByIdAsync(routeTouristPlaceId, cancellationToken);
        if (routeTouristPlace is null)
        {
            throw new RouteTouristPlaceNotFoundException(routeTouristPlaceId);
        }
        _repositoryManager.RouteTouristPlaceRepository.Remove(routeTouristPlace);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}