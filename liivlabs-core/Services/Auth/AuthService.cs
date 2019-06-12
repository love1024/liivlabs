using liivlabs_shared.Interfaces.Repository.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// AUth service to handle
/// Authentication of user
/// </summary>
namespace liivlabs_core.Services.Auth
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Configuration 
        /// </summary>
        IConfiguration configuration;

        /// <summary>
        /// Create Auth Service
        /// </summary>
        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Create Hash of password
        /// and output both hash and salt used
        /// to generate hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            HMACSHA512 hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Issue new token
        /// </summary>
        /// <returns></returns>
        public string IssueNewToken()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(this.configuration["Secret"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Verify given password using stored hash and salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            HMACSHA512 hmac = new HMACSHA512(storedSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }
    }
}
