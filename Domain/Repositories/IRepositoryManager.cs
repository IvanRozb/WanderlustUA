namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
