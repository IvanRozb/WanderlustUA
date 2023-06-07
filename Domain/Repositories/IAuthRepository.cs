using Contracts;
using Domain.Entities;

namespace Domain.Repositories;

public interface IAuthRepository
{
    public Task<Auth> Register(UserForRegistrationDto userForRegistration, string passwordKey);
    public Task<Guid> Login(UserForLoginDto userForLogin, string passwordKey);
}