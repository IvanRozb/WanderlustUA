using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

public class JointService : IJointService
{
    private readonly IRepositoryManager _repositoryManager;
    public JointService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<JointDto>> GetAllByUserRouteIdAsync(Guid userId, Guid routeId, CancellationToken cancellationToken = default)
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

        var joints = await _repositoryManager.JointRepository.GetAllByRouteIdAsync(routeId, cancellationToken);
        var jointsDto = joints.Adapt<IEnumerable<JointDto>>();
        return jointsDto;
    }

    public async Task<JointDto> GetByIdAsync(Guid userId, Guid routeId, Guid jointId, CancellationToken cancellationToken)
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

        var joint = await _repositoryManager.JointRepository.GetByIdAsync(jointId, cancellationToken);
        if (joint is null)
        {
            throw new JointNotFoundException(jointId);
        }
        if (joint.RouteId != routeId)
        {
            throw new JointDoesNotBelongToRouteException(jointId, routeId);
        }
        
        var jointDto = joint.Adapt<JointDto>();
        return jointDto;
    }

    public async Task<JointDto> CreateAsync(Guid userId, Guid routeId, Guid touristPlaceId, JointForCreationDto jointForCreationDto,
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
        
        var jointDto = jointForCreationDto.Adapt<Joint>();
        jointDto.RouteId = routeId;
        jointDto.TouristPlaceId = touristPlaceId;
        _repositoryManager.JointRepository.Insert(jointDto);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return jointDto.Adapt<JointDto>();
    }

    public async Task DeleteAsync(Guid userId, Guid routeId, Guid jointId, CancellationToken cancellationToken = default)
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

        var joint =
            await _repositoryManager.JointRepository.GetByIdAsync(jointId, cancellationToken);
        if (joint is null)
        {
            throw new JointNotFoundException(jointId);
        }
        _repositoryManager.JointRepository.Remove(joint);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}