using Contracts;

namespace Domain.Repositories;

public interface IAuthRepository
{
    public void Register(UserForRegistrationDto userForRegistration, string passwordKey);
}