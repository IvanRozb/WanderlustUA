using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public UserRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.Users.ToListAsync(cancellationToken);

        public async Task<User> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default) =>
            await _dbContext.Users.FirstAsync(x => x.Id == ownerId, cancellationToken);

        public void Insert(User owner) => _dbContext.Users.Add(owner);

        public void Remove(User owner) => _dbContext.Users.Remove(owner);
    }
}
