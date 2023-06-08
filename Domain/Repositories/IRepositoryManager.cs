namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }
        ITouristPlaceRepository TouristPlaceRepository { get; }
        IRouteRepository RouteRepository { get; }
        
        IUnitOfWork UnitOfWork { get; }
    }
}
