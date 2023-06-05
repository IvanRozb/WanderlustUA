using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Create(User user);
    void Update(User user);
    void Delete(User user);
}