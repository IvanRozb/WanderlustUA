namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
