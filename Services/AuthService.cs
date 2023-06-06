using Domain.Repositories;
using Services.Abstractions;

namespace Services;

internal sealed class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    public AuthService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
}