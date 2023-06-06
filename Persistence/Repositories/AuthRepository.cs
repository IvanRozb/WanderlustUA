using System.Security.Cryptography;
using System.Text;
using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Persistence.Repositories;

internal sealed class AuthRepository : IAuthRepository
{        
    private readonly RepositoryDbContext _dbContext;
    public AuthRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async void Register(UserForRegistrationDto userForRegistration, string passwordKey)
    {
        var passwordSalt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(passwordSalt);
        
        var passwordHash = GetPasswordHash(userForRegistration.Password,
            passwordSalt, passwordKey);

        var authEntity = new Auth
        {
            Email = userForRegistration.Email,
            PassHash = passwordHash,
            PassSalt = passwordSalt
        };
        await _dbContext.Auth.AddAsync(authEntity);

        var user = userForRegistration.Adapt<User>();
        await _dbContext.Users.AddAsync(user);
    }
    
    private static byte[] GetPasswordHash(string password, byte[] passwordSalt, string passwordKey)
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