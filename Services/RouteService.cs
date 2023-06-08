using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

internal sealed class RouteService : IRouteService
{
    private readonly IRepositoryManager _repositoryManager;
    public RouteService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    public async Task<IEnumerable<RouteDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var routes = await _repositoryManager.RouteRepository.GetAllByUserIdAsync(userId, cancellationToken);
        var routesDto = routes.Adapt<IEnumerable<RouteDto>>();
        return routesDto;
    }
    public async Task<RouteDto> GetByIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken)
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
        if (route.UserId != user.Id)
        {
            throw new RouteDoesNotBelongToUserException(user.Id, route.Id);
        }
        var routeDto = route.Adapt<RouteDto>();
        return routeDto;
    }
    public async Task<RouteDto> CreateAsync(Guid userId, RouteForCreationDto routeForCreationDto, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var route = routeForCreationDto.Adapt<Route>();
        route.UserId = user.Id;
        _repositoryManager.RouteRepository.Insert(route);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return route.Adapt<RouteDto>();
    }
    public async Task DeleteAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default)
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
        if (route.UserId != user.Id)
        {
            throw new RouteDoesNotBelongToUserException(user.Id, route.Id);
        }
        _repositoryManager.RouteRepository.Remove(route);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
