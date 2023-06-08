﻿using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IUserRepository> _lazyUserRepository;
        private readonly Lazy<IAuthRepository> _lazyAuthRepository;
        private readonly Lazy<ITouristPlaceRepository> _lazyTouristPlaceRepository;
        private readonly Lazy<IRouteRepository> _lazyRouteRepository;
        private readonly Lazy<IRouteTouristPlaceRepository> _lazyRouteTouristPlaceRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthRepository(dbContext));
            _lazyTouristPlaceRepository = new Lazy<ITouristPlaceRepository>(() => new TouristPlaceRepository(dbContext));
            _lazyRouteRepository = new Lazy<IRouteRepository>(() => new RouteRepository(dbContext));
            _lazyRouteTouristPlaceRepository = new Lazy<IRouteTouristPlaceRepository>(() => new RouteTouristPlaceRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IUserRepository UserRepository => _lazyUserRepository.Value;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;
        public ITouristPlaceRepository TouristPlaceRepository => _lazyTouristPlaceRepository.Value;
        public IRouteRepository RouteRepository => _lazyRouteRepository.Value;
        public IRouteTouristPlaceRepository RouteTouristPlaceRepository => _lazyRouteTouristPlaceRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
