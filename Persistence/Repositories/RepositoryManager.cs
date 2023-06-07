using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IUserRepository> _lazyUserRepository;
        private readonly Lazy<IAuthRepository> _lazyAuthRepository;
        private readonly Lazy<ITouristPlaceRepository> _lazyTouristPlaceRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _lazyAuthRepository = new Lazy<IAuthRepository>(() => new AuthRepository(dbContext));
            _lazyTouristPlaceRepository = new Lazy<ITouristPlaceRepository>(() => new TouristPlaceRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IUserRepository UserRepository => _lazyUserRepository.Value;
        public IAuthRepository AuthRepository => _lazyAuthRepository.Value;
        public ITouristPlaceRepository TouristPlaceRepository => _lazyTouristPlaceRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
