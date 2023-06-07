using Contracts;
using Domain.Entities;

namespace Services.Abstractions;

public interface IAuthService
{
    public Task<Auth> Register(UserForRegistrationDto userForRegistration, string passwordKey);
    public Task<Dictionary<string, string>> Login(UserForLoginDto userForLogin, string passwordKey);

}