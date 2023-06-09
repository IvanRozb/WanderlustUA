using System.Security.Claims;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

internal sealed class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    public AuthService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<Auth> Register(UserForRegistrationDto userForRegistration, string passwordKey)
    {
        if (userForRegistration.Password != userForRegistration.PasswordConfirm)
            throw new IncorrectPasswordException("Error: Password and Confirm Password fields do not match. " +
                                "Please make sure the passwords match and try again");

        var authEntity = await _repositoryManager.AuthRepository
            .Register(userForRegistration, passwordKey);
        
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return authEntity;
    }

    public async Task<Dictionary<string, string>> Login(UserForLoginDto userForLogin, string passwordKey,
        string tokenKey, string adminKey)
    {
        var userId = await _repositoryManager.AuthRepository.Login(userForLogin, passwordKey);
        
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return new Dictionary<string, string> {
            {"token", AuthHelper.CreateToken(userId, tokenKey, adminKey)}
        };
    }
}