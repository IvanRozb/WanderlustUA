using System.Security.Cryptography;
using System.Text;
using Contracts;
using Domain.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repositories;

internal sealed class AuthRepository : IAuthRepository
{        
    private readonly RepositoryDbContext _dbContext;
    public AuthRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public void Register(UserForRegistrationDto userForRegistration, string passwordKey)
    {
        var passwordSalt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(passwordSalt);
        
        var passwordHash = GetPasswordHash(userForRegistration.Password,
            passwordSalt, passwordKey);
    }
    
    private static IEnumerable<byte> GetPasswordHash(string password, byte[] passwordSalt, string passwordKey)
    {
        var passwordSaltPlusString = passwordKey + Convert.ToBase64String(passwordSalt);

        return KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 1000000,
            numBytesRequested: 256 / 8
        );
    }
}