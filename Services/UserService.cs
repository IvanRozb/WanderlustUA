using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    public UserService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _repositoryManager.UserRepository.GetAllAsync(cancellationToken);

        var usersDto = users.Adapt<IEnumerable<UserDto>>();

        return usersDto;
    }

    public async Task<UserDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        var userDto = user.Adapt<UserDto>();
        return userDto;
    }
    public async Task<UserDto> CreateAsync(UserForCreationDto userForCreationDto, CancellationToken cancellationToken = default)
    {
        var user = userForCreationDto.Adapt<User>();
        user.IsActive = true;
        user.CreatedAt = DateTime.Now;
        _repositoryManager.UserRepository.Insert(user);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return user.Adapt<UserDto>();
    }
    public async Task UpdateAsync(Guid userId, UserForUpdateDto userForUpdateDto, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        user.Username = userForUpdateDto.Username;
        user.FirstName = userForUpdateDto.FirstName;
        user.LastName = userForUpdateDto.LastName;
        user.Email = userForUpdateDto.Email;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }
        _repositoryManager.UserRepository.Remove(user);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}