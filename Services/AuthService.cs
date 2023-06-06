using Contracts;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

internal sealed class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    public AuthService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async void Register(UserForRegistrationDto userForRegistration, string passwordKey)
    {
        if (userForRegistration.Password != userForRegistration.PasswordConfirm)
            throw new Exception("Error: Password and Confirm Password fields do not match. " +
                                "Please make sure the passwords match and try again");

        if (await _repositoryManager.UserRepository.DoesUserWithEmailExist(userForRegistration.Email))
            throw new Exception("User with this email does already exist.");

        _repositoryManager.AuthRepository
            .Register(userForRegistration, passwordKey);
    }
}