using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
    
    public static string CreateToken(Guid userId)
    {
        var claims = new Claim[] {
            new("userId", userId.ToString())
        };

        // string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;
        //
        // SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
        //     Encoding.UTF8.GetBytes(
        //         tokenKeyString != null ? tokenKeyString : ""
        //     )
        // );
        //
        // SigningCredentials credentials = new SigningCredentials(
        //     tokenKey, 
        //     SecurityAlgorithms.HmacSha512Signature
        // );
        //
        // SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        // {
        //     Subject = new ClaimsIdentity(claims),
        //     SigningCredentials = credentials,
        //     Expires = DateTime.Now.AddDays(1)
        // };
        //
        // JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //
        // SecurityToken token = tokenHandler.CreateToken(descriptor);

        return "tokenHandler.WriteToken(token)";

    }
}