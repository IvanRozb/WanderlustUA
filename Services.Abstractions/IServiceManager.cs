namespace Services.Abstractions;

public interface IServiceManager
{
    IUserService UserService { get; }
    IAuthService AuthService { get; }
    IRouteService RouteService { get; }
    ITouristPlaceService TouristPlaceService { get; }
    IRouteTouristPlaceService RouteTouristPlaceService { get; }
}