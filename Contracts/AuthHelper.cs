using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace Contracts;

public static class AuthHelper
{
    public static byte[] GetPasswordHash(string password, byte[] passwordSalt, string passwordKey)
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
    
    public static string CreateToken(Guid userId, string tokenKeyString)
    {
        var claims = new Claim[] {
            new("userId", userId.ToString())
        };

        var tokenKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenKeyString)
        );
        var credentials = new SigningCredentials(
            tokenKey, 
            SecurityAlgorithms.HmacSha256Signature
        );

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(1)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);

    }
}