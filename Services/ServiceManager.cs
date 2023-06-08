using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IAuthService> _lazyAuthService;
        private readonly Lazy<ITouristPlaceService> _lazyTouristPlaceService;
        private readonly Lazy<IRouteService> _lazyRouteService;
        private readonly Lazy<IJointService> _lazyJointService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyUserService = new Lazy<IUserService>(() => 
                new UserService(repositoryManager));
            _lazyAuthService = new Lazy<IAuthService>(() => 
                new AuthService(repositoryManager));
            _lazyTouristPlaceService = new Lazy<ITouristPlaceService>(() => 
                new TouristPlaceService(repositoryManager));
            _lazyRouteService = new Lazy<IRouteService>(() => 
                new RouteService(repositoryManager));
            _lazyJointService = new Lazy<IJointService>(() => 
                new JointService(repositoryManager));
        }

        public IUserService UserService => _lazyUserService.Value;
        public IAuthService AuthService => _lazyAuthService.Value;
        public ITouristPlaceService TouristPlaceService => _lazyTouristPlaceService.Value;
        public IRouteService RouteService => _lazyRouteService.Value;
        public IJointService JointService => _lazyJointService.Value;
    }
}
