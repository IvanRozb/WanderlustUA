using Contracts;

namespace Services.Abstractions;

public interface IAuthService
{
    public void Register(UserForRegistrationDto userForRegistration, string passwordKey);
}