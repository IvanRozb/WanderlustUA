namespace Services.Abstractions;

public interface IServiceManager
{
    IUserService UserService { get; }
    IAuthService AuthService { get; }
    ITouristPlaceService TouristPlaceService { get; }
}