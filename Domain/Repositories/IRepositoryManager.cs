namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository OwnerRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
